using System.Collections;
using UnityEngine;

public class AxeDestroy : MonoBehaviour
{
    [SerializeField] private float timeToDestroyAfterCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Environment")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(DestroyAxe());
        }
    }

    private IEnumerator DestroyAxe()
    {
        yield return new WaitForSeconds(timeToDestroyAfterCollision);
        Destroy(gameObject);
    }
}
