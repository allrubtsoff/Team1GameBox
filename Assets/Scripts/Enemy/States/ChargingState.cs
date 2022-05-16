using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingState : EnemyStates
{
    public ChargingState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {
        Vector3 lastPlayerPos = _enemyController.m_Target.position;
        _enemyController.Agent.SetDestination(lastPlayerPos);
        _enemyController.Agent.isStopped = true;
        _enemyController.Agent.speed = Vector3.Distance(_enemyController.Agent.destination, _enemyController.transform.position) /
                                                                    _enemyController.specialAnimLength;
        _enemyController.CreatePond(lastPlayerPos);
        yield return new WaitForSeconds(_enemyController.enemiesConfigs.specialAttackDelay);
        _enemyController.StartSpecialAttack();
    }

    public override IEnumerator SpecialBehaviour()
    {
        
        yield break;
    }
}
