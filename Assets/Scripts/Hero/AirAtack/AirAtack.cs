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
            if (CreateMarker != null)
            {
                //Ray ray = new Ray(transform.position, new Vector3(transform.position.x, transform.position.y -2f,transform.position.z));
                //Debug.DrawRay(transform.position, new Vector3(transform.position.x,0, transform.position.z), Color.red, 10f);
                //Physics.Raycast(ray, out RaycastHit raycastHit, 999f);
                    CreateMarker(new Vector3(transform.position.x, 0, transform.position.z), 1f);
            }
        }
        else
            ResetAirAtack();
    }

    private void ResetAirAtack()
    {
        if (animatorManager.isGrounded())
        {
            animatorManager.SetAirAtack(false);
        }
    }
}
