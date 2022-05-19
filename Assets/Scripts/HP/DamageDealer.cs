using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health))
            health.TakeDamage(damage);
    }
}
