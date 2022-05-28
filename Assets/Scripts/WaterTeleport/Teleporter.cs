using StarterAssets;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _damage = 10;
    [SerializeField] private Saver _saver;
    [SerializeField] private float _teleportDelay = 0.5f;
    [Header("CheckPoints")]
    [SerializeField] private CheckPoint[] checkPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<ThirdPersonController>(out ThirdPersonController playerController))
        {
            StartCoroutine(TelerportToSafePosition(playerController.transform));
        }
    }

    private IEnumerator TelerportToSafePosition(Transform player)
    {
        yield return new WaitForSeconds(_teleportDelay);
        if (player.TryGetComponent<IDamageable>(out IDamageable playerHealth))
            playerHealth.TakeDamage(_damage, _playerLayer);

        Vector3 spawnPosition = checkPoints[0].SpawnPoint.position;

        foreach (var point in checkPoints)
        {
            if (_saver.CheckPointToSave == point.PointNumber)
            {
                spawnPosition = point.SpawnPoint.position;
                break;
            }
        }

        player.position = spawnPosition;
    }
}
