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

        _enemyController.Agent.isStopped = true;

        yield break;
    }
}
