using StarterAssets;
using System;
using UnityEngine;

[RequireComponent(typeof(MeleeAtack))]
public class AirAtack : MonoBehaviour
{    
    private AnimatorManager animatorManager;
    private StarterAssetsInputs inputs;

    public bool IsAirAtack { get { return !animatorManager.isGrounded() && inputs.atack; } }

    public static event Action<Vector3, float> CreateMarker;

    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<MeleeAtack>().GetAnimatorManager();
    }

    void Update()
    {
        CheckAirAtack();
    }

    private void CheckAirAtack()
    {
        if (IsAirAtack)
        {
            animatorManager.SetAirAtack(IsAirAtack);
        }
        else
            ResetAirAtack();
    }

    private void ResetAirAtack()
    {
        if (animatorManager.GetAirAtack() && animatorManager.isGrounded())
        {
            animatorManager.SetAirAtack(false);
            if (CreateMarker != null)
            {
                CreateMarker(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z) + transform.forward, 1f);
            }
        }
    }
}
