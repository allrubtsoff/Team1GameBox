using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRayAttack : EnemyStates
{
    public SpecialRayAttack(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {
        _enemyController.Agent.isStopped = false;
        yield break;
    }
}
