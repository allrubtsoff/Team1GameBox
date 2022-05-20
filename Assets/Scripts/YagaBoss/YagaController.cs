using System.Collections;
using System;
using UnityEngine;

public class YagaController : YagaStateMachine
{
    [SerializeField] public  Transform Target;
    [SerializeField] public  float AttackDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private AnimationClip attackAnimation;

    private const float _lookHeight = 1f;

    private float _markerDelay;

    private const int _deadState = 0;
    private const int _idleState = 1;
    private const int _attackState = 2;
    private const int _castState = 3;
    private const int _callState = 4;

    private bool _isAttacking;
    private bool _isAttackCooled;

    public static event Action<Quaternion, float> CreateConeMarker;

    public int CurrentState { get; private set; }
    public float CastDealy { get; set; }

    private void Start()
    {
        CurrentState = _idleState;
        _isAttackCooled = true;
        _markerDelay = attackAnimation.length;
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case _idleState:
                Vector3 playerPos = Target.position;
                playerPos.y = _lookHeight;
                transform.LookAt(playerPos);



                if (_isAttackCooled && Vector3.Distance(playerPos, transform.position) < AttackDistance)
                {
                    CurrentState = _attackState;
                    _isAttackCooled = false;
                }
                break;
            case _attackState:
                if (!_isAttacking)
                {
                    _isAttacking = true;

                    if (CreateConeMarker != null)
                        CreateConeMarker(transform.rotation, _markerDelay);

                }
                break;
            case _castState:

                break;

            case _callState:

                break;
            case _deadState:

                break;
        }
    }

    public void ChangeState()
    {
        _isAttacking = false;
        CurrentState = _idleState;
        StartCoroutine(AttackCooling(_attackCooldown));
    }

    private IEnumerator AttackCooling(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        _isAttackCooled = true;
    }
}
