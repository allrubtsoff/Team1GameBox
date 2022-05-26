using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(Energy))]
public class MIghtyPunch : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private GameObject prefab;
    [SerializeField] private UnityEvent mightyPunchEvent;
    [Space (20)]
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float hitRadius = 1.5f;
    [SerializeField] private float hitDistance = 1.5f;
    [SerializeField] private int countToDamage = 10;

    private StarterAssetsInputs playerInputs;
    private AnimatorManager animatorManager;
    private Energy energy;
    private PunchDamage punch;

    private bool isMightyPunchCooled = true;

    void Start()
    {
        playerInputs = GetComponent<StarterAssetsInputs>();
        animatorManager = GetComponent<AnimatorManager>();
        energy = GetComponent<Energy>();
        punch = prefab.GetComponent<PunchDamage>();
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

    public void Kick()
    {
        float height = transform.position.y + transform.localScale.y;
        Vector3 rayPos = new Vector3(transform.position.x, height, transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward);
        RaycastHit[] hits = new RaycastHit[countToDamage];
        if (Physics.SphereCastNonAlloc(ray, hitRadius, hits, hitDistance, _mask) > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != null && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
                {

                    Debug.Log($"Kick {hit.transform.name}");
                    damageable.TakeDamage(configs.mightyPunchDamage, _mask);
                    Vector3 pushVector = hit.transform.position - transform.position;
                    hit.transform.GetComponent<NavMeshAgent>().velocity = pushVector.normalized * configs.mightyPunchForce;
#if (UNITY_EDITOR)
                    Debug.Log("hit " + hit.transform.name);
#endif
                }
            }
        }

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
        if (animatorManager.GetMightyPunch())
            playerInputs.move = Vector2.zero;
    }

    public void ResetMightyPunch()
    {
        playerInputs.mightyPunch = false;
        animatorManager.SetMightyPunch(false);
    }
}
