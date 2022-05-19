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

    private const int AngleOfView = 60;

    private StarterAssetsInputs playerInputs;
    private ThirdPersonController playerController;
    private bool isBackward;


    private void Start()
    {
        playerInputs = animator.gameObject.GetComponent<StarterAssetsInputs>();
        playerController = animator.gameObject.GetComponent<ThirdPersonController>();
    }

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

        if (playerInputs.move != Vector2.zero && playerController.Grounded)
        {       
            isBackward = mouseManager.AngleBetweenMouseAndPlayer > AngleOfView;
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
