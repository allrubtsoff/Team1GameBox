using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private readonly int atack = Animator.StringToHash("Atack");
    private readonly int axeThrow = Animator.StringToHash("AxeThrow");
    private readonly int axeAim = Animator.StringToHash("AxeAim");

    public void SetAtack(bool value)
    {
        animator.SetBool(atack, value);
    }

    public void SetAxeThrow(bool value)
    {
        animator.SetBool(axeThrow, value);
    }

    public void SetAxeAim(bool value)
    {
        animator.SetBool(axeAim, value);
    }
}
