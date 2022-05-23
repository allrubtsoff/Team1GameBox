using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeleeAtack))]
public class AirAtack : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;

    private AnimatorManager animatorManager;
    private StarterAssetsInputs inputs;
    private Energy energy;
    private bool isAirAtackCooled = true;

    public bool IsAirAtack { get { return !animatorManager.isGrounded() && inputs.atack; } }

    public UnityEvent UpdateUI;

    public static event Action<Vector3, float> CreateMarker;

    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<AnimatorManager>();
        energy = GetComponent<Energy>();
    }

    void Update()
    {
        CheckAirAtack();
    }

    private void CheckAirAtack()
    {
        if (AirAtackAvailable())
        {
            animatorManager.SetAirAtack(IsAirAtack);
        }
        TryUseAirAtack();
    }

    private bool AirAtackAvailable()
    {
        return IsAirAtack && energy.CheckEnergyAvailable(configs.airAtackCost) && isAirAtackCooled;
    }

    private void TryUseAirAtack()
    {
        if (animatorManager.GetAirAtack() && animatorManager.isGrounded())
        {
            UpdateUI.Invoke();
            animatorManager.SetAirAtack(false);
            if (CreateMarker != null)
            {
                CreateMarker(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z) + transform.forward, 1f);
            }
            energy.UseEnergy(configs.airAtackCost);
            StartCoroutine(BlockThrowAxe());
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        isAirAtackCooled = false;
        yield return new WaitForSecondsRealtime(configs.airAtackCooldown);
        isAirAtackCooled = true;
    }

    private IEnumerator BlockThrowAxe()
    {
        yield return new WaitForEndOfFrame();
        inputs.throwAxe = false;
    }
}
