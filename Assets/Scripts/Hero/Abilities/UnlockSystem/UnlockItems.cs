using UnityEngine;

public class UnlockItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent<UnlockSystemManager>(out UnlockSystemManager unlockManager))
            unlockManager.TryUnlock();
    }
}
