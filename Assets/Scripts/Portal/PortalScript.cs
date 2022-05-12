using System.Collections;
using UnityEngine;
using StarterAssets;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private GameObject interactSymbol;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private Transform teleportationTarget;
    bool isTeleported = false;

    private void Start()
    {
        interactSymbol.SetActive(false);
    }

    public void SetTeleportTimer()
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
                other.transform.position = teleportationTarget.position;

                if (teleportationTarget.gameObject.GetComponent<PortalScript>() != null)
                {
                    teleportationTarget.gameObject.GetComponent<PortalScript>().SetTeleportTimer();
                }
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
