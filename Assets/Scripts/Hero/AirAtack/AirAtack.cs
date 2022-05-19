using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAtack : MonoBehaviour
{    
    [SerializeField] private AnimatorManager animatorManager;

    private StarterAssetsInputs inputs;
    private ThirdPersonController personController;


    public bool IsAirAtack { get { return !personController.Grounded && inputs.atack;} }
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        personController = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        CheckAirAtack();
        ResetAirAtack();
    }

    private void CheckAirAtack()
    {
        if (IsAirAtack)
        {
            animatorManager.SetAirAtack(IsAirAtack);
        }
    }

    private void ResetAirAtack()
    {
        if (personController.Grounded)
            animatorManager.SetAirAtack(false); ;
    }
}
