using System.Collections;
using System;
using UnityEngine;

public class YagaController : YagaStateMachine
{
    [SerializeField] public Transform Target;
    [SerializeField] private SpellCaster _kettle;
    [SerializeField] public float AttackDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private AnimationClip _attackAnimation;

    private const float _lookHeight = 1f;
    private const int _spellCount = 4;

    private float _markerDelay;

    private const int _deadState = 0;
    private const int _idleState = 1;
    private const int _attackState = 2;
    private const int _castState = 3;
    private const int _callState = 4;

    private int _lastSpell;
    private const int _clowdSpell = 0;
    private const int _multyConeSpell = 1;
    private const int _pondSpell = 2;
    private const int _ConesNPondSpell = 3;



    private bool _isAttacking;
    private bool _isAttackCooled;

    public static event Action<Quaternion, float> CreateConeMarker;

    public int CurrentState { get; private set; }
    public float CastDealy { get; set; }

    public bool IsAlive { get; set; }

    private void Start()
    {
        CurrentState = _idleState;
        _isAttackCooled = true;
        _markerDelay = _attackAnimation.length;
        IsAlive = true;
        StartCoroutine(SpellCaster());
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case _idleState:
                Vector3 playerPos = Target.position;
                playerPos.y = _lookHeight;
                transform.LookAt(playerPos);

                break;
            //case _attackState:
            //    if (!_isAttacking)
            //    {
            //        _isAttacking = true;

            //        if (CreateConeMarker != null)
            //            CreateConeMarker(transform.rotation, _markerDelay);

            //    }
            //    break;
            case _castState:
                Vector3 castPos = _kettle.transform.position;
                castPos.y = _lookHeight;
                transform.LookAt(castPos);
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

    private IEnumerator SpellCaster()
    {
        yield return new WaitForSeconds(2f);
        while (IsAlive)
        {
            if (_isAttackCooled && !_isAttacking)
            {
                _isAttackCooled = false;
                CurrentState = _castState;

            }
        }
        yield break;
    }

   
}
