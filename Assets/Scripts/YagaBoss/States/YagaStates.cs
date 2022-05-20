using System.Collections;

public abstract class YagaStates : YagaStateMachine
{
    protected YagaController _yagaController;

    public YagaStates(YagaController yagaController)
    {
        _yagaController = yagaController;
    }

    public virtual IEnumerator CurrentState()
    {
        yield break;
    }


}
