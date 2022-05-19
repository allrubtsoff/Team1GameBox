using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : EnemyStates
{
    public RangeAttackState(EnemyController enemyController) : base(enemyController)
    {
    }


    public override IEnumerator CurrentState()
    {
        _enemyController.Agent.updateRotation = true;
        _enemyController.Agent.isStopped = true;

        yield break;
    }
}
