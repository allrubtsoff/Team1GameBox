using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : EnemyStates
{
    public AttackState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {
        if (!_enemyController.IsAttaking)
        {
            Debug.Log("Regular Attack");
            _enemyController.IsAttaking = true;
        }
        _enemyController.Agent.isStopped = true;

        yield break;
    }
}
