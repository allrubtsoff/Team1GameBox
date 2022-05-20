using StarterAssets;
using System;
using UnityEngine;

public class AirAtack : MonoBehaviour
{    
    [SerializeField] private AnimatorManager animatorManager;

    private StarterAssetsInputs inputs;

    public bool IsAirAtack { get { return !animatorManager.isGrounded() && inputs.atack; } }

    public static event Action<Vector3, float> CreateMarker;

    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
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
                CreateMarker(new Vector3(transform.position.x, 0, transform.position.z) + transform.forward, 1f);
            }
        }
    }
}
