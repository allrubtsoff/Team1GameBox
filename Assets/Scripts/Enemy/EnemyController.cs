using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : EnemyStateMachine
{

    [SerializeField] public Transform m_Target;
    [SerializeField] public EnemiesConfigs enemiesConfigs;
    [SerializeField] public GameObject pondPrefab;
    [Header("Marker")]
    [Header("EnemyType")]
    [SerializeField] private bool isLikho;
    [SerializeField] private bool isCyberGiant;
    [SerializeField] private bool isCreature;
    [Header("SpecialAttackAnimation")]
    [SerializeField] private AnimationClip specialAttack;
    public NavMeshAgent Agent { get; set; }
    public int CurrState { get; private set; }
    public bool DoSpecial { get; set; }
    public float specialAnimLength { get; private set; }

    private const int idleState = 0;
    private const int moveState = 1;
    private const int attackState = 2;
    private const int specialJumpState = 3;
    private const int chargeState = 4;
    private const int deadState = 8;

    private bool _isAttaking;
    private bool _isCharging;
    private bool _isSpecialAttacking;
    private bool _isSpecialAttackCooled;

    private void OnEnable()
    {
        EnemyAnimations.IsAttacking += SetIsAttacking;
        EnemyAnimations.TrySpecialEvent += TakeASpecialChance;
    }

    private void OnDisable()
    {
        EnemyAnimations.IsAttacking -= SetIsAttacking;
        EnemyAnimations.TrySpecialEvent -= TakeASpecialChance;
    }

    private void TakeASpecialChance(bool chance) => SpecialAttackChance();
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = enemiesConfigs.speed;
        Agent.stoppingDistance = enemiesConfigs.stoppingDistance;
        CurrState = idleState;
        specialAnimLength = specialAttack.length;
        _isSpecialAttackCooled = true;
        _isCharging = false;
        _isSpecialAttacking = false;
    }

    private void Update()
    {
        if (isLikho)
        {
            LikhoControll();
        }
    }

    private void LikhoControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, m_Target.position);

        Debug.Log( $"Current state {CurrState}");

        if (CurrState != chargeState)
        {
            _isCharging = false;
        }

        switch (CurrState)
        {
            case idleState:
                if (distanceToTarget <= enemiesConfigs.reactDistance)
                {
                    CurrState = moveState;
                }
                SetState(new IdleState(this));
                break;
            case moveState:
                if (distanceToTarget <= Agent.stoppingDistance)
                {
                    CurrState = attackState;
                }
                SetState(new MoveState(this));
                break;
            case attackState:
                if (distanceToTarget > Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = moveState;
                }
                SetState(new AttackState(this));
                break;

            case chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;
                    SetState(new ChargingState(this));
                }
                break;
            case specialJumpState:

                float newDistance = Vector3.Distance(transform.position, Agent.destination);

                if (newDistance <= Agent.stoppingDistance)
                {
                    Agent.velocity = Vector3.zero;
                    CurrState = moveState;
                }

                SetState(new SpecialJumpAttack(this));
                break;
            case deadState:

                break;
        }
    }

    private void SetIsAttacking(bool isAttacking)
    {
        _isAttaking = isAttacking;
        if (_isAttaking == false)
        {
            SpecialAttackChance();
        }
    }

    public void SpecialAttackChance()
    {
        Debug.Log("TryToSpecialAttack");
        if (_isSpecialAttackCooled && UnityEngine.Random.value < enemiesConfigs.specialAttackChance)
        {
            _isSpecialAttackCooled = false;
            CurrState = chargeState;
        }
    }

    public void StartSpecialAttack()
    {
        CurrState = specialJumpState;
        StartCoroutine(SpecialAttackCooling(enemiesConfigs.specialAttackCooldownTime));
    }

    public void CreatePond(Vector3 lastTargetPos)
    {
        lastTargetPos.y += 0.01f;
        GameObject pond = Instantiate(pondPrefab, lastTargetPos  , Quaternion.identity);
        Destroy(pond, enemiesConfigs.specialAttackDelay + specialAnimLength);
    }

    private IEnumerator SpecialAttackCooling(float specialAttackReloadTime)
    {
        yield return new WaitForSeconds(specialAttackReloadTime);
        _isSpecialAttackCooled = true;
    }
}
