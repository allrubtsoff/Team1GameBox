using StarterAssets;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Energy))]
public class MIghtyPunch : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private GameObject prefab;

    private StarterAssetsInputs playerInputs;
    private AnimatorManager animatorManager;
    private Energy energy;
    private GameObject marker;

    private bool isMightyPunchCooled = true;

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
        if (isMightyPunchAvailable())
        {
            Punch();
        }
    }

    private bool isMightyPunchAvailable()
    {
        return playerInputs.mightyPunch && energy.CheckEnergyAvailable(configs.mightyPunchCost)
            && isMightyPunchCooled && animatorManager.isGrounded();
    }

    private void Punch()
    {
        energy.UseEnergy(configs.mightyPunchCost);
        //start animation
        marker = Instantiate(prefab,gameObject.transform);
        CircleDamage();
        Destroy(marker, 1f);
        StartCoroutine(CoolDown());
    }

    private void CircleDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, configs.mightyPunchRange, configs.enemyLayer);
        if(enemies.Length > 0)
            foreach (var enemie in enemies)
            {
                if (enemie.gameObject.TryGetComponent<Health>(out Health health))
                {
                    health.TakeDamage(configs.mightyPunchDamage);
                }
            }
    }

    private IEnumerator CoolDown()
    {
        isMightyPunchCooled = false;
        yield return new WaitForSeconds(configs.mightyPunchCooldown);
        playerInputs.mightyPunch = false;
        isMightyPunchCooled = true;
    }
}
