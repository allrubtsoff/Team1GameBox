using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Energy))]
public class MIghtyPunch : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private GameObject prefab;
    [SerializeField] private UnityEvent mightyPunchEvent;

    private StarterAssetsInputs playerInputs;
    private AnimatorManager animatorManager;
    private Energy energy;

    private bool isMightyPunchCooled = true;

    void Start()
    {
        playerInputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<AnimatorManager>();
        energy = GetComponent<Energy>();
    }

    void Update()
    {
        CheckMightyPunch();
    }

    private void CheckMightyPunch()
    {
        if (isMightyPunchAvailable())
        {
            Punch();
            TryUpdateUI();
        }
        StopMovement();
    }

    private bool isMightyPunchAvailable()
    {
        return playerInputs.mightyPunch && energy.CheckEnergyAvailable(configs.mightyPunchCost)
            && isMightyPunchCooled && animatorManager.isGrounded();
    }

    private void TryUpdateUI()
    {
        if (mightyPunchEvent != null)
            mightyPunchEvent.Invoke();
    }

    private void Punch()
    {
        energy.UseEnergy(configs.mightyPunchCost);
        animatorManager.SetMightyPunch(true);
        StartCoroutine(ShowPunchZone());
        StartCoroutine(CoolDown());
    }

    private IEnumerator ShowPunchZone()
    {
        prefab.SetActive(true);
        yield return new WaitForSeconds(1f);
        prefab.SetActive(false);
    }

    private IEnumerator CoolDown()
    {
        isMightyPunchCooled = false;
        yield return new WaitForSeconds(configs.mightyPunchCooldown);
        playerInputs.mightyPunch = false;
        isMightyPunchCooled = true;
    }

    private void StopMovement()
    {
        if(animatorManager.GetMightyPunch())
            playerInputs.move = Vector2.zero;
    }

    public void ResetMightyPunch()
    {
        playerInputs.mightyPunch = false;
        animatorManager.SetMightyPunch(false);
    }
}
