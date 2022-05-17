using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : EnemyStates
{
    public ChargingState(EnemyController enemyController) : base(enemyController)
    {
    }

    public static event Action<Vector3, float> CreateMarker;

    private const float yCorrection = 0.01f;

    public override IEnumerator CurrentState()
    {
        Vector3 lastPlayerPos = _enemyController.m_Target.position;
        Vector3 markerTarget = new Vector3(lastPlayerPos.x, lastPlayerPos.y + yCorrection, lastPlayerPos.z);
        float deleteTime = _enemyController.enemiesConfigs.specialAttackDelay + _enemyController.specialAnimLength;
        CreateMarker(markerTarget,deleteTime);
        _enemyController.Agent.isStopped = true;
        _enemyController.Agent.SetDestination(lastPlayerPos);
        float newSpeed = Vector3.Distance(_enemyController.Agent.destination, _enemyController.transform.position) /
                                                                    _enemyController.specialAnimLength;
        _enemyController.Agent.speed = newSpeed;
        yield return new WaitForSeconds(_enemyController.enemiesConfigs.specialAttackDelay);
        _enemyController.StartSpecialAttack();
    }

    public override IEnumerator SpecialBehaviour()
    {
        
        yield break;
    }
}
