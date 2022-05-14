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

    private int currState;
    private const int idleState = 0;
    private const int moveState = 1;
    private const int attackState = 2;
    private const int specialAttackState = 3;
    private const int deadState = 4;

    public bool IsAttaking;

    public NavMeshAgent Agent { get; set; }



    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = enemiesConfigs.speed;
        currState = idleState;
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

        switch (currState)
        {
            case idleState:
                if (distanceToTarget < enemiesConfigs.reactDistance)
                {
                    currState = moveState;
                }
                SetState(new IdleState(this));
                break;
            case moveState:
                if (distanceToTarget < Agent.stoppingDistance)
                {
                    currState = attackState;
                }
                SetState(new MoveState(this));
                break;
            case attackState:
                if (distanceToTarget > Agent.stoppingDistance && !IsAttaking)
                {
                    currState = moveState;
                }

                if (Random.value < enemiesConfigs.specialAttackChance)
                {
                    currState = specialAttackState;
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
