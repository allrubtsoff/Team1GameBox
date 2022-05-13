using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private MousePositionManager mousePositionManager;

    private StarterAssetsInputs input;

    private Vector3 mouseWorldPosition = Vector3.zero;
    private RaycastHit raycastHit;

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
        if (input.atack)
        {
            Vector3 target = new Vector3(mousePositionManager.MousePosition.x, transform.position.y, mousePositionManager.MousePosition.z);
            transform.LookAt(target);
            animatorManager.SetAtack(true);
        }
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackState()
    {
        input.atack = false;
        animatorManager.SetAtack(false);
    }
}
