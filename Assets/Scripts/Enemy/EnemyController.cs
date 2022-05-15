using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyStateMachine
{
    [SerializeField] public Transform m_Target { get; }
    [SerializeField] private EnemiesConfigs enemiesConfigs;
    [Header("EnemyType")]
    [SerializeField] private bool isLikho;
    [SerializeField] private bool isCyberGiant;
    [SerializeField] private bool isCreature;

    public Animator Animator { get; set; }

    public int CurrState { get; private set; }
    private const int idleState = 0;
    private const int moveState = 1;
    private const int attackState = 2;
    private const int specialJumpState = 3;
    private const int deadState = 4;

    public bool IsAttaking;

    public NavMeshAgent Agent { get; set; }



    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = enemiesConfigs.speed;
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
                SetState(new IdleState(this));
                break;
            case moveState:
                if (distanceToTarget < Agent.stoppingDistance)
                {
                    CurrState = attackState;
                }
                SetState(new MoveState(this));
                break;
            case attackState:
                if (distanceToTarget > Agent.stoppingDistance && !IsAttaking)
                {
                    CurrState = moveState;
                }

                if (Random.value < enemiesConfigs.specialAttackChance)
                {
                    CurrState = specialJumpState;
                }
                SetState(new AttackState(this));
                break;
            case deadState:

                break;
        }

    }

    private IEnumerator AttackCooling(float attackClipLength)
    {
        yield return new WaitForSeconds(attackClipLength);
        IsAttaking = false;
    }
}
