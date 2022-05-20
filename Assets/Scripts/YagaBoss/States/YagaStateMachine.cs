using UnityEngine;

public abstract class YagaStateMachine : MonoBehaviour
{

    protected YagaStates _yagaStates;

    public void SetState(YagaStates states)
    {
        _yagaStates = states;
        StartCoroutine(_yagaStates.CurrentState());
    }
}
