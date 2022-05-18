using StarterAssets;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MousePositionManager mouseManager;

    private readonly int atack = Animator.StringToHash("Atack");
    private readonly int axeThrow = Animator.StringToHash("AxeThrow");
    private readonly int axeAim = Animator.StringToHash("AxeAim");    
    private readonly int airAtack = Animator.StringToHash("AirAtack");

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

    public void SetAirAtack(bool value)
    {
        animator.SetBool(airAtack, value);
    }

    public void CheckBackwardRun()
    {
        var player = animator.gameObject.GetComponent<StarterAssetsInputs>();
        var controller = animator.gameObject.GetComponent<ThirdPersonController>();
        if (player.move != Vector2.zero && controller.Grounded)
        {       

            //Debug.Log(mouseManager.AngleBetweenMouseAndPlayer);
            var isBackward = mouseManager.AngleBetweenMouseAndPlayer > 90 ;
            if (isBackward)
                SetBackwardRun();
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
