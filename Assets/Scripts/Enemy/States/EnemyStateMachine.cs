using UnityEngine;


public abstract class EnemyStateMachine : MonoBehaviour
{
    protected EnemyStates _enemyStates;

    public void SetState(EnemyStates state)
    {
        _enemyStates = state;
        StartCoroutine(_enemyStates.CurrentState());
    }
}
