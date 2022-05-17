using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : EnemyStateMachine
{

    [SerializeField] public Transform m_Target;
    [SerializeField] public EnemiesConfigs enemiesConfigs;
    [Header("EnemyType")]
    [SerializeField] private bool isLikho;
    [SerializeField] private bool isCyberGiant;
    [SerializeField] private bool isSniper;
    [Header("SpecialAttackAnimation")]
    [SerializeField] private AnimationClip specialAttack;
    public NavMeshAgent Agent { get; set; }
    public int CurrState { get; private set; }
    public bool DoSpecial { get; set; }
    public float specialAnimLength { get; private set; }

    private enum EnemyType
    {
        Likho,
        CyberGiant,
        Sniper,
        Normal
    }
    private int enemyType;

    private const int idleState = 0;
    private const int moveState = 1;
    private const int attackState = 2;
    private const int specialJumpState = 3;
    private const int chargeState = 4;
    private const int deadState = 8;

    private float stopDistanceCorrection = 0.3f;

    private bool _isAttaking;
    private bool _isCharging;
    private bool _isSpecialAttacking;
    private bool _isSpecialAttackCooled;

    private void OnEnable()
    {
        EnemyAnimations.IsAttacking += SetIsAttacking;
        EnemyAnimations.TrySpecialEvent += TakeASpecialChance;
        EnemyAnimations.SpecialIsFinished += SpecialJumpIsFinished;
    }

    private void OnDisable()
    {
        EnemyAnimations.IsAttacking -= SetIsAttacking;
        EnemyAnimations.TrySpecialEvent -= TakeASpecialChance;
        EnemyAnimations.SpecialIsFinished += SpecialJumpIsFinished;
    }

    private void TakeASpecialChance(bool chance) => SpecialAttackChance();
    private void SpecialJumpIsFinished() => _isSpecialAttacking = false;


    private void Start()
    {
        EnemyTypeSet();
        EnemyPresetsOnType();
    }

    private void EnemyPresetsOnType()
    {
        Agent = GetComponent<NavMeshAgent>();

        switch (enemyType)
        {
            case (int)EnemyType.Likho:
                Agent.speed = enemiesConfigs.likhoSpeed;
                Agent.stoppingDistance = enemiesConfigs.likhoStoppingDistance;
                break;
            case (int)EnemyType.CyberGiant:
                Agent.speed = enemiesConfigs.giantSpeed;
                Agent.stoppingDistance = enemiesConfigs.giantStoppingDistance;
                break;
            case (int)EnemyType.Sniper:
                Agent.speed = enemiesConfigs.sniperSpeed;
                Agent.stoppingDistance = enemiesConfigs.sniperStoppingDistance;
                break;
            case (int)EnemyType.Normal:
                Agent.speed = enemiesConfigs.normalSpeed;
                Agent.stoppingDistance = enemiesConfigs.normalStoppingDistance;
                break;
        }

        stopDistanceCorrection += Agent.stoppingDistance;
        CurrState = idleState;
        specialAnimLength = specialAttack.length;
        _isSpecialAttackCooled = true;
        _isCharging = false;
        _isSpecialAttacking = false;
    }

    private void EnemyTypeSet()
    {
        if (isLikho)
        {
            enemyType = (int)EnemyType.Likho;
        }
        else if (isCyberGiant)
        {
            enemyType = (int)EnemyType.CyberGiant;
        }
        else if (isSniper)
        {
            enemyType = (int)EnemyType.Sniper;
        }
        else
        {
            enemyType = (int)EnemyType.Normal;
        }
    }

    private void Update()
    {
        EnemyBehaviour();
    }

    private void EnemyBehaviour()
    {

        switch (enemyType)
        {
            case (int)EnemyType.Likho:
                LikhoControll();
                break;
            case (int)EnemyType.CyberGiant:
                Agent.speed = enemiesConfigs.giantSpeed;
                Agent.stoppingDistance = enemiesConfigs.giantStoppingDistance;
                break;
            case (int)EnemyType.Sniper:
                SniperControll();
                break;
            case (int)EnemyType.Normal:
                Agent.speed = enemiesConfigs.likhoSpeed;
                Agent.stoppingDistance = enemiesConfigs.likhoStoppingDistance;
                break;
        }
    }

    private void SniperControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, m_Target.position);

        if (CurrState != chargeState)
        {
            _isCharging = false;
        }

        switch (CurrState)
        {
            case idleState:
                if (distanceToTarget <= enemiesConfigs.sniperReactDistance)
                {
                    CurrState = moveState;
                }
                SetState(new IdleState(this));
                break;
            case moveState:
                SetState(new MoveState(this));
                if (distanceToTarget <= Agent.stoppingDistance)
                {
                    CurrState = attackState;
                }
                break;
            case attackState:
                SetState(new MeleeAttackState(this));
                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = moveState;
                }
                break;
            case chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;
                    SetState(new ChargingState(this));
                }
                break;
            case specialJumpState:

                SetState(new SpecialJumpAttack(this));

                float newDistance = Vector3.Distance(transform.position, Agent.destination);

                if (!_isSpecialAttacking)
                {
                    Agent.velocity = Vector3.zero;
                    CurrState = moveState;
                }

                break;
            case deadState:

                break;
        }
    }

    private void LikhoControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, m_Target.position);

        if (CurrState != chargeState)
        {
            _isCharging = false;
        }

        switch (CurrState)
        {
            case idleState:
                if (distanceToTarget <= enemiesConfigs.likhoReactDistance)
                {
                    CurrState = moveState;
                }
                SetState(new IdleState(this));
                break;
            case moveState:
                SetState(new MoveState(this));
                if (distanceToTarget <= stopDistanceCorrection)
                {
                    CurrState = attackState;
                }
                break;
            case attackState:
                SetState(new MeleeAttackState(this));
                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = moveState;
                }
                break;
            case chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;
                    SetState(new ChargingState(this));
                }
                break;
            case specialJumpState:
                
                SetState(new SpecialJumpAttack(this));

                float newDistance = Vector3.Distance(transform.position, Agent.destination);

                if (!_isSpecialAttacking)
                {
                    Agent.velocity = Vector3.zero;
                    CurrState = moveState;
                }

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
        if (_isSpecialAttackCooled && UnityEngine.Random.value < enemiesConfigs.likhoSpecialAttackChance)
        {
            _isSpecialAttackCooled = false;
            CurrState = chargeState;
        }
    }

    public void StartSpecialAttack()
    {
        _isSpecialAttacking = true;
        CurrState = specialJumpState;
        StartCoroutine(SpecialAttackCooling(enemiesConfigs.likhoSpecialAttackCooldownTime));
    }


    private IEnumerator SpecialAttackCooling(float specialAttackReloadTime)
    {
        yield return new WaitForSeconds(specialAttackReloadTime);
        _isSpecialAttackCooled = true;
    }


}
