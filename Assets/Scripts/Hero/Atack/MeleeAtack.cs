using StarterAssets;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private MousePositionManager mousePositionManager;
    [SerializeField] private float atackTime;

    private StarterAssetsInputs input;
    private ThirdPersonController controller;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        Atack();
    }

    private void Atack()
    {
        if (input.atack && controller.Grounded)
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
}
