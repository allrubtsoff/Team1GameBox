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
            Debug.Log(hits.Length);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null && hit.transform.TryGetComponent<Health>(out Health health))
                {
                    IDamageable damageable = health;
                    damageable.TakeDamage(damage, _damageTo);
                    Debug.Log(hit);
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
