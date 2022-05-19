using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyStateMachine
{

    [SerializeField] public Transform Target;
    [SerializeField] public EnemiesConfigs EnemiesConfigs;
    [Header("EnemyType")]
    [SerializeField] private bool _isLikho;
    [SerializeField] private bool _isCyberGiant;
    [Header("SpecialAttackAnimation")]
    [Tooltip("Special Animation of your enemy type")]
    [SerializeField] private AnimationClip _specialAttack;
    private enum EnemyType
    {
        Likho,
        CyberGiant,
        Normal
    }
    private int _enemyType;

    private const int _idleState = 0;
    private const int _moveState = 1;
    private const int _attackState = 2;
    private const int _specialState = 3;
    private const int _chargeState = 4;
    private const int _deadState = 8;

    private const float yPlayerCorrection = 1f;

    private float _stopDistanceCorrection = 0.3f;

    private float _specialChance;
    private float _cooldownTime;

    private bool _isAttaking;
    private bool _isCharging;
    private bool _isSpecialAttacking;
    private bool _isSpecialAttackCooled;



    public NavMeshAgent Agent { get; set; }
    public int CurrState { get; private set; }
    public bool DoSpecial { get; set; }
    public float SpecialAnimLength { get; private set; }
    public void SpecialIsFinished() => _isSpecialAttacking = false;


    private void Start()
    {
        EnemyTypeSet();
        EnemyPresetsOnType();
    }

    private void EnemyPresetsOnType()
    {
        Agent = GetComponent<NavMeshAgent>();

        switch (_enemyType)
        {
            case (int)EnemyType.Likho:
                Agent.speed = EnemiesConfigs.likhoSpeed;
                Agent.stoppingDistance = EnemiesConfigs.likhoStoppingDistance;
                _specialChance = EnemiesConfigs.likhoSpecialAttackChance;
                _cooldownTime = EnemiesConfigs.likhoSpecialAttackCooldownTime;
                break;
            case (int)EnemyType.CyberGiant:
                Agent.speed = EnemiesConfigs.giantSpeed;
                Agent.stoppingDistance = EnemiesConfigs.giantStoppingDistance;
                _specialChance = EnemiesConfigs.giantSpecialAttackChance;
                _cooldownTime = EnemiesConfigs.giantSpecialAttackCooldownTime;
                break;
            case (int)EnemyType.Normal:
                Agent.speed = EnemiesConfigs.normalSpeed;
                Agent.stoppingDistance = EnemiesConfigs.normalStoppingDistance;
                break;
        }

        _stopDistanceCorrection += Agent.stoppingDistance;
        CurrState = _idleState;
        SpecialAnimLength = _specialAttack.length;
        _isSpecialAttackCooled = true;
        _isCharging = false;
        _isSpecialAttacking = false;
    }

    private void EnemyTypeSet()
    {
        if (_isLikho)
        {
            _enemyType = (int)EnemyType.Likho;
        }
        else if (_isCyberGiant)
        {
            _enemyType = (int)EnemyType.CyberGiant;
        }
        else
        {
            _enemyType = (int)EnemyType.Normal;
        }
    }

    private void Update()
    {
        EnemyBehaviour();
    }

    private void EnemyBehaviour()
    {

        switch (_enemyType)
        {
            case (int)EnemyType.Likho:
                LikhoControll();
                break;
            case (int)EnemyType.CyberGiant:
                GiantControll();
                break;
            case (int)EnemyType.Normal:
                Agent.speed = EnemiesConfigs.normalSpeed;
                Agent.stoppingDistance = EnemiesConfigs.normalStoppingDistance;
                break;
        }
    }

    private void GiantControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        if (CurrState != _chargeState)
        {
            _isCharging = false;
        }

        switch (CurrState)
        {
            case _idleState:
                if (distanceToTarget <= EnemiesConfigs.giantReactDistance)
                {
                    CurrState = _moveState;
                }
                SetState(new IdleState(this));
                break;
            case _moveState:
                SetState(new MoveState(this));
                if (distanceToTarget <= Agent.stoppingDistance)
                {
                    CurrState = _attackState;
                }
                break;
            case _attackState:
                SetState(new AttackState(this));

                Vector3 lastPlayerPos = Target.position;
                lastPlayerPos.y += yPlayerCorrection;
                transform.LookAt(lastPlayerPos);

                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = _moveState;
                }

                break;
            case _chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;

                    switch (_enemyType)
                    {
                        case (int)EnemyType.Likho:
                            SetState(new LikhoChargingState(this));
                            break;
                        case (int)EnemyType.CyberGiant:
                            SetState(new GiantChargeState(this));
                            break;
                    }
                }
                break;
            case _specialState:

                SetState(new SpecialJumpAttack(this));

                if (!_isSpecialAttacking)
                {
                    Agent.velocity = Vector3.zero;
                    CurrState = _moveState;
                }

                break;
            case _deadState:

                break;
        }
    }

    private void LikhoControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        if (CurrState != _chargeState)
        {
            _isCharging = false;
        }

        switch (CurrState)
        {
            case _idleState:
                if (distanceToTarget <= EnemiesConfigs.likhoReactDistance)
                {
                    CurrState = _moveState;
                }
                SetState(new IdleState(this));
                break;
            case _moveState:
                SetState(new MoveState(this));
                if (distanceToTarget <= _stopDistanceCorrection)
                {
                    CurrState = _attackState;
                }
                break;
            case _attackState:
                SetState(new AttackState(this));
                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = _moveState;
                }
                break;
            case _chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;
                    SetState(new LikhoChargingState(this));
                }
                break;
            case _specialState:

                SetState(new SpecialJumpAttack(this));

                float newDistance = Vector3.Distance(transform.position, Agent.destination);

                if (!_isSpecialAttacking)
                {
                    Agent.velocity = Vector3.zero;
                    CurrState = _moveState;
                }

                break;
            case _deadState:

                break;
        }
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _isAttaking = isAttacking;
        if (_isAttaking == false)
        {
            SpecialAttackChance();
        }
    }

    public void SpecialAttackChance()
    {
        bool canDoSpecial = _isSpecialAttackCooled && UnityEngine.Random.value < _specialChance;

        if (canDoSpecial)
        {
            _isSpecialAttackCooled = false;
            CurrState = _chargeState;
        }
    }

    public void StartSpecialAttack()
    {
        _isSpecialAttacking = true;
        CurrState = _specialState;
        StartCoroutine(SpecialAttackCooling(_cooldownTime));
    }


    private IEnumerator SpecialAttackCooling(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        _isSpecialAttackCooled = true;
    }


}
