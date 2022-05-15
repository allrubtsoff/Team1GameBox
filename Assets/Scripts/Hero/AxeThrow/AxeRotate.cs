using StarterAssets;
using System.Collections;
using UnityEngine;

public class AxeRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeToDestroyAfterCollision;

    private float xRotation;

    public bool Activated { get; set; }

    void Update()
    {
        if (Activated)
        {
            xRotation += rotationSpeed;
            transform.localRotation = Quaternion.Euler(xRotation, transform.rotation.y, transform.rotation.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag =="Environment")
        {
            Activated = false;
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
