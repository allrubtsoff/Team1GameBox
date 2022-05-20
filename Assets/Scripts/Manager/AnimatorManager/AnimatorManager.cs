using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MousePositionManager mouseManager;

    private readonly int atack = Animator.StringToHash("Atack");
    private readonly int axeThrow = Animator.StringToHash("AxeThrow");
    private readonly int axeAim = Animator.StringToHash("AxeAim");    
    private readonly int airAtack = Animator.StringToHash("AirAtack");
    private readonly int animatorSpeed = Animator.StringToHash("Speed");
    private readonly int grounded = Animator.StringToHash("Grounded");

    private const int AngleOfView = 60;
    private bool backward;

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

    public bool isGrounded()
    {
        return animator.GetBool(grounded);
    }

    public void SetAirAtack(bool value)
    {
        animator.SetBool(airAtack, value);
    }

    public bool GetAirAtack()
    {
        return animator.GetBool(airAtack);
    }

    public void CheckBackwardRun()
    {
        if (animator.GetFloat(animatorSpeed) > 0  && isGrounded())
        {
            backward = mouseManager.AngleBetweenMouseAndPlayer > AngleOfView;
            if (backward)
            {
                SetBackwardRun();
            }
        }
    }

    private void SetBackwardRun()
    {
        animator.SetLayerWeight(2, 1);
    }

    public void ResetBackwardRun()
    {
        animator.SetLayerWeight(2, 0);
    }
}
