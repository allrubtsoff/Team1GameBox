using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float bulletSpeed;

    private const float k_targetHeightCorrection = 1f;

    private void OnEnable() => EnemyAnimations.ShootEvent += Shoot;

    private void OnDisable() => EnemyAnimations.ShootEvent -= Shoot;

    private void Shoot( Vector3 target)
    {
        target.y += k_targetHeightCorrection;
        GameObject newBullet = Instantiate(bullet, shootPosition.position, Quaternion.identity);
        var rb = newBullet.GetComponent<Rigidbody>();
        Vector3 pushVector = target - shootPosition.position;
        rb.AddForce(pushVector * bulletSpeed, ForceMode.Impulse);
        Destroy(newBullet, 2.5f);
    }
}
