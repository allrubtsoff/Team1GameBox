using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YagaIdleState : YagaStates
{
    public YagaIdleState(YagaController yagaController) : base(yagaController)
    {
    }

    private const float _lookHeight = 1f;

    public override IEnumerator CurrentState()
    {

        yield break;
        
    }
}
