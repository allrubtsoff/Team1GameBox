using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PondMarkerScript : MonoBehaviour
{
    private ThirdPersonController _thirdPersonController;
    private bool _isPlayerInZone;

    private const float kHeigth = 0.001f;

    public void TryToHit(int dmg)
    {
        if (_isPlayerInZone)
        {
            Debug.Log("PLAYER HITTED " + dmg);
        }
    }

    public void Resize(float width, float length)
    {
        transform.localScale = new Vector3(width, kHeigth, length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController thirdPersonController))
        {
            _isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController thirdPersonController))
        {
            _isPlayerInZone = false;
        }
    }
}
