using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< HEAD
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
=======
        if (other.TryGetComponent<Health>(out Health health))
            health.TakeDamage(damage);
>>>>>>> parent of def3c50 (Damage SphereCast)
    }
}
