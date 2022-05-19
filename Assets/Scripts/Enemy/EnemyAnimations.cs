using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private EnemyController m_EnemyController;
    [SerializeField] private EnemyShooter m_Shooter;

    private Animator _animator;
    private int _animIDIdle;
    private int _animIDMove;
    private int _animIDAttack;
    private int _animIDSpecialJump;

    private int _currAnimState;
    private const int _idleState = 0;
    private const int _moveState = 1;
    private const int _attackState = 2;
    private const int _specialState = 3;
    private const int _chargeState = 4;
    private const int _deadState = 8;


    public void Shoot() => m_Shooter.Shoot(m_EnemyController.Target.position);
    public void SpecialFinished() => m_EnemyController.SpecialIsFinished();
    public void TrySpecial() => m_EnemyController.SpecialAttackChance();
    public void StartAttackEvent() => m_EnemyController.SetIsAttacking(true);
    public void StopAttackEvent() => m_EnemyController.SetIsAttacking(false);

    void Start()
    {
        _animator = GetComponent<Animator>();

        AssignAnimationsIDs();
    }

    private void Update()
    {
        _currAnimState = m_EnemyController.CurrState;
        if (_animator != null)
        {
            StateSwitcher();
        }
    }

    private void StateSwitcher()
    {
        switch (_currAnimState)
        {
            case _idleState:
                IdleState();
                break;
            case _chargeState:
                IdleState();
                break;
            case _moveState:
                MoveState();
                break;
            case _attackState:
                AttackState();
                break;
            case _specialState:
                SpecialState();
                break;
            case _deadState:

                break;
        }
    }

    private void AssignAnimationsIDs()
    {
        _animIDIdle = Animator.StringToHash("Idle");
        _animIDMove = Animator.StringToHash("Move");
        _animIDAttack = Animator.StringToHash("Attack");
        _animIDSpecialJump = Animator.StringToHash("Special");
    }

    public void IdleState()
    {
        _animator.SetBool(_animIDIdle, true);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecialJump, false);
    }

    public void AttackState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, true);
        _animator.SetBool(_animIDSpecialJump, false);
    }

    public void MoveState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, true);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecialJump, false);
    }

    public void SpecialState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecialJump, true);
    }

}
