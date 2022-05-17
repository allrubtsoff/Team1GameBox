using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttackState : EnemyStates
{
    public MeleeAttackState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {

        _enemyController.Agent.isStopped = true;

        yield break;
    }
}
