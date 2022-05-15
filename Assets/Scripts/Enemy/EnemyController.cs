using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : EnemyStateMachine
{

    [SerializeField] public Transform m_Target;
    [SerializeField] private EnemiesConfigs enemiesConfigs;
    [Header("EnemyType")]
    [SerializeField] private bool isLikho;
    [SerializeField] private bool isCyberGiant;
    [SerializeField] private bool isCreature;
    public NavMeshAgent Agent { get; set; }
    public int CurrState { get; private set; }

    private const int idleState = 0;
    private const int moveState = 1;
    private const int attackState = 2;
    private const int specialJumpState = 3;
    private const int deadState = 4;

    private bool _isAttaking;

    private void OnEnable()
    {
        EnemyAnimations.IsAttacking += SetIsAttacking;
    }

    private void OnDisable()
    {
        EnemyAnimations.IsAttacking -= SetIsAttacking;
    }

    private void SetIsAttacking(bool isAttacking) => _isAttaking = isAttacking;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = enemiesConfigs.speed;
        Agent.stoppingDistance = enemiesConfigs.stoppingDistance;
        CurrState = idleState;
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

        switch (CurrState)
        {
            case idleState:
                if (distanceToTarget < enemiesConfigs.reactDistance)
                {
                    CurrState = moveState;
                }
                Debug.Log("Idle");
                SetState(new IdleState(this));
                break;
            case moveState:
                if (distanceToTarget < Agent.stoppingDistance)
                {
                    CurrState = attackState;

                }
                Debug.Log("Move");
                SetState(new MoveState(this));
                break;
            case attackState:
                if (distanceToTarget > Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = moveState;
                }

                if (UnityEngine.Random.value < enemiesConfigs.specialAttackChance)
                {
                    CurrState = specialJumpState;

                }

                Debug.Log("Attack");
                SetState(new AttackState(this));
                break;

            case specialJumpState:
                Debug.Log("SpecialJump");

                break;
            case deadState:

                break;
        }
    }


    public void ChangeIsAttack()
    {
        if (_isAttaking)
        {
            _isAttaking = false;
        }
        else
        {
            _isAttaking = true;
        }
    }

    private IEnumerator AttackCooling(float attackClipLength)
    {
        yield return new WaitForSeconds(attackClipLength);
        _isAttaking = false;
    }
}
