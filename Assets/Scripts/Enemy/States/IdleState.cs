using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyStates
{
    public IdleState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {
        _enemyController.Agent.isStopped = true;

        yield break;
    }
}
