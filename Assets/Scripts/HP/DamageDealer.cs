using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask _damageTo;
    [SerializeField] private float hitRadius;
    [SerializeField] private float hitDistance;
    [SerializeField] private int countToDamage;

    public void AttackSphereCast()
    {
        float height = 1;
        Vector3 rayPos = new Vector3(transform.position.x, height , transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward);
        RaycastHit[] hits = new RaycastHit[countToDamage];
        if (Physics.SphereCastNonAlloc(ray, hitRadius, hits, hitDistance, _damageTo) < countToDamage)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage, _damageTo);
                }
            }
        } 
    }

    private void OnDrawGizmos()
    {
        float height = transform.position.y + transform.localScale.y;
        Vector3 rayPos = new Vector3(transform.position.x, height, transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward);
        RaycastHit[] hits = new RaycastHit[countToDamage];
        if (Physics.SphereCastNonAlloc(ray, hitRadius, hits, hitDistance, _damageTo) > 0)
        {
           
        }
    }

}
