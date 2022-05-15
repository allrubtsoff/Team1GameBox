using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialJumpAttack : EnemyStates
{
    public SpecialJumpAttack(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {

        yield break;
    }
}
