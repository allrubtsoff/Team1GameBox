using StarterAssets;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private MousePositionManager mousePositionManager;
    [SerializeField] private float atackTime;

    private ThirdPersonController personController;
    private StarterAssetsInputs input;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        personController = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        Atack();
    }

    private void Atack()
    {
        if (input.atack)
        {
            animatorManager.SetAtack(true);
            LookAtMouseDirection();
        }
    }

    private void LookAtMouseDirection()
    {
        personController.isAtacking = true;
        Vector3 target = new Vector3(mousePositionManager.MousePosition.x, transform.position.y, mousePositionManager.MousePosition.z);
        transform.LookAt(target);
    }

    //to reset player rotation
    private void resetRotationState()
    {
        personController.isAtacking = false;
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackState()
    {
        input.atack = false;
        animatorManager.SetAtack(false);
    }
}
