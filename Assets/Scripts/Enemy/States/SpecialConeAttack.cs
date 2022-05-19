using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialConeAttack : EnemyStates
{
    public SpecialConeAttack(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {

        yield break;
    }
}
