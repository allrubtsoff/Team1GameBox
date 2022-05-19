using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikhoChargingState : EnemyStates
{
    public LikhoChargingState(EnemyController enemyController) : base(enemyController)
    {
    }

    public static event Action<Vector3, float> CreateMarker;

    private const float yCorrection = 0.01f;

    public override IEnumerator CurrentState()
    {
        Vector3 lastPlayerPos = _enemyController.Target.position;
        Vector3 markerTarget = new Vector3(lastPlayerPos.x, lastPlayerPos.y + yCorrection, lastPlayerPos.z);
        float deleteTime = _enemyController.EnemiesConfigs.likhoSpecialAttackDelay +
                            _enemyController.SpecialAnimLength;
        CreateMarker(markerTarget, deleteTime);
        _enemyController.Agent.isStopped = true;
        _enemyController.Agent.SetDestination(lastPlayerPos);
        float newSpeed = Vector3.Distance(_enemyController.Agent.destination, _enemyController.transform.position) /
                                                                    _enemyController.SpecialAnimLength;
        _enemyController.Agent.speed = newSpeed;
        yield return new WaitForSeconds(_enemyController.EnemiesConfigs.likhoSpecialAttackDelay);
        _enemyController.StartSpecialAttack();
    }
}
