using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private EnemyController m_EnemyController;
    private Animator animator;
    private int animIDIdle;
    private int animIDMove;
    private int animIDAttack;
    private int animIDSpecialJump;

    private int currAnimState;
    private const int idleState = 0;
    private const int moveState = 1;
    private const int attackState = 2;
    private const int specialJumpState = 3;
    private const int deadState = 4;

    void Start()
    {
        m_EnemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        AssignAnimationsIDs();
    }

    private void Update()
    {
        currAnimState = m_EnemyController.CurrState;
        if (animator != null)
        {
            StateSwitcher();
        }
    }

    private void StateSwitcher()
    {
        switch (currAnimState)
        {
            case idleState:
                IdleState();
                break;
            case moveState:
                MoveState();
                break;
            case attackState:
                AttackState();
                break;
            case specialJumpState:
                SpecialJumpState();
                break;
        }
    }

    private void AssignAnimationsIDs()
    {
        animIDIdle = Animator.StringToHash("Idle");
        animIDMove = Animator.StringToHash("Move");
        animIDAttack = Animator.StringToHash("Attack");
        animIDSpecialJump = Animator.StringToHash("SpecialJump");
    }

    public void IdleState()
    {
        animator.SetBool(animIDIdle, true);
        animator.SetBool(animIDMove, false);
        animator.SetBool(animIDAttack, false);
        animator.SetBool(animIDSpecialJump, false);
    }

    public void AttackState()
    {
        animator.SetBool(animIDIdle, false);
        animator.SetBool(animIDMove, false);
        animator.SetBool(animIDAttack, true);
        animator.SetBool(animIDSpecialJump, false);
    }

    public void MoveState()
    {
        animator.SetBool(animIDIdle, false);
        animator.SetBool(animIDMove, true);
        animator.SetBool(animIDAttack, false);
        animator.SetBool(animIDSpecialJump, false);
    }

    public void SpecialJumpState()
    {
        animator.SetBool(animIDIdle, false);
        animator.SetBool(animIDMove, false);
        animator.SetBool(animIDAttack, false);
        animator.SetBool(animIDSpecialJump, true);
    }


}
