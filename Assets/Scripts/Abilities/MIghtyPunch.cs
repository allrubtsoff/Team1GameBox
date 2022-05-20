using StarterAssets;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Energy))]
public class MIghtyPunch : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;

    private StarterAssetsInputs playerInputs;
    private AnimatorManager animatorManager;
    private Energy energy;

    private bool isMightyPunchCooled;

    void Start()
    {
        playerInputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<MeleeAtack>().GetAnimatorManager();
        energy = GetComponent<Energy>();
    }

    void Update()
    {
        CheckMightyPunch();
    }

    private void CheckMightyPunch()
    {
        if (playerInputs.mightyPunch && energy.CheckEnergyAvailable(configs.mightyPunchCost))
        {
            Punch();
        }
    }

    private void Punch()
    {
        //start animation
        //Damage Dealing logic
        StartCoroutine(CoolDown());
    }

    private void CircleDamage()
    {

    }

    private IEnumerator CoolDown()
    {
        isMightyPunchCooled = false;    
        yield return new WaitForSeconds(configs.mightyPunchCooldown);
        isMightyPunchCooled = true;
    }
}
