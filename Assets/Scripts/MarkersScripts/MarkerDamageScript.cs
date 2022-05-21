using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class MarkerDamageScript : MonoBehaviour
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

    public void PondResize(float radius)
    {
        transform.localScale = new Vector3(radius, kHeigth, radius);
    }

    public void RayResize(float width, float length)
    {
        transform.localScale = new Vector3(width, length, 1);
    }

    public void ConeResize(float width, float length)
    {
        transform.localScale = new Vector3(width, 1, length);
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
