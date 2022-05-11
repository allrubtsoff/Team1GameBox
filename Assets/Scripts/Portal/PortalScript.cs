using System.Collections;
using UnityEngine;
using StarterAssets;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private GameObject interactSymbol;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private Transform teleportationTarget;
    bool isTeleported = false;

    public delegate void TeleportCallback();
    public static event TeleportCallback TeleportUsed;

    private void OnEnable() => PortalScript.TeleportUsed += TeleportTimer;
    private void OnDisable() => PortalScript.TeleportUsed -= TeleportTimer;

    private void Start()
    {
        interactSymbol.SetActive(false);
    }

    private void TeleportTimer()
    {
        isTeleported = true;
        StartCoroutine(TeleportTimerCorutine());
    } 

    private IEnumerator TeleportTimerCorutine()
    {
        yield return new WaitForSeconds(1f);
        isTeleported = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactSymbol.SetActive(true);

            if (input.interact && !isTeleported)
            {
                TeleportUsed();
                other.transform.position = teleportationTarget.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactSymbol.SetActive(false);
        }
    }
}
