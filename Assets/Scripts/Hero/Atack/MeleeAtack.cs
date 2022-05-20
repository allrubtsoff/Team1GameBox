using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private MousePositionManager mousePositionManager;

    private StarterAssetsInputs input;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        Atack();
    }

    private void Atack()
    {
        if (input.atack && animatorManager.isGrounded())
        {
            animatorManager.SetAtack(true);
            mousePositionManager.LookAtMouseDirection();
            animatorManager.CheckBackwardRun();
        }
    }

    //to reset player rotation
    private void resetRotationState()
    {
        mousePositionManager.StopLookingAtMouseDirection();
        animatorManager.ResetBackwardRun();
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackState()
    {
        input.atack = false;
        animatorManager.SetAtack(false);
    }

    public AnimatorManager GetAnimatorManager() 
        => this.animatorManager;
    
    public MousePositionManager GetMouseManager() 
        => this.mousePositionManager;
}
