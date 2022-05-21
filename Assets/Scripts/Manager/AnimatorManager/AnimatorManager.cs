using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MousePositionManager mouseManager;

    private readonly int atack = Animator.StringToHash("Atack");
    private readonly int axeThrow = Animator.StringToHash("AxeThrow"); 
    private readonly int airAtack = Animator.StringToHash("AirAtack");
    private readonly int mightyPunch = Animator.StringToHash("MightyPunch");
    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int grounded = Animator.StringToHash("Grounded");

    private const int AngleOfView = 90;
    private bool backward;

    public void SetAtack(bool value)
    {
        animator.SetBool(atack, value);
        SetAxeThrow(false);
    }

    public void SetAxeThrow(bool value)
    {
        animator.SetBool(axeThrow, value);
    }

    public bool isGrounded()
    {
        return animator.GetBool(grounded);
    }

    public void SetAirAtack(bool value)
    {
        animator.SetBool(airAtack, value);
        SetAxeThrow(false);
        SetAtack(false);
    }

    public void SetMightyPunch(bool value)
    {
        animator.SetBool(mightyPunch, value);
    }

    public bool GetMightyPunch()
    {
        return animator.GetBool(mightyPunch);
    }

    public bool GetAirAtack()
    {
        return animator.GetBool(airAtack);
    }

    public void CheckBackwardRun()
    {
        if (animator.GetFloat(Speed) > 0  && isGrounded())
        {
            backward = mouseManager.GetAngleBetweenMouseAndPlayer() > AngleOfView;
            
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
