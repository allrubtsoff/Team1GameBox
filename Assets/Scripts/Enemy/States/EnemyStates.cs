using System.Collections;

public abstract class EnemyStates
{
    protected EnemyController _enemyController;

    public EnemyStates(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public virtual IEnumerator CurrentState()
    {
        yield break;
    }

    public virtual IEnumerator SpecialBehaviour()
    {
        yield break;
    }
}
