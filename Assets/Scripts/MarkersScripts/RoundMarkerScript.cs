using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class RoundMarkerScript : MonoBehaviour
{
    private bool _isPlayerInZone;
    private ThirdPersonController _thirdPersonController;

    public void TryToHit()
    {
        if (_isPlayerInZone)
        {
            Debug.Log("PLAYER HITTED");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out ThirdPersonController personController))
        {
            _isPlayerInZone = true;
            _thirdPersonController = personController;
        }
        else
        {
            _isPlayerInZone = false;
        }
    }
}
