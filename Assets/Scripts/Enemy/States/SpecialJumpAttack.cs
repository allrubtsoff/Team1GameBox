using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpecialJumpAttack : EnemyStates
{
    public SpecialJumpAttack(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {
        _enemyController.Agent.isStopped = false;
        yield break;
    }
}
