using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpecialJumpAttack : EnemyStates
{
    public SpecialJumpAttack(EnemyController enemyController) : base(enemyController)
    {
    }

    private const float minDistance = 0.5f;

    public override IEnumerator CurrentState()
    {
        if (Vector3.Distance(_enemyController.TmpTarget, _enemyController.transform.position) < minDistance)
        {
            _enemyController.IsSpecialJumping = false;
            _enemyController.Agent.enabled = true;
        }
        yield break;
    }
}
