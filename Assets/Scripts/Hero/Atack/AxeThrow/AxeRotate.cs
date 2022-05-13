using System.Collections;
using UnityEngine;

public class AxeRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeToDestroyAfterCollision;
    [SerializeField] private Transform playerHand;
    [SerializeField] private GameObject player;

    private float xRotation;

    public bool Activated { get; set; }

    void Update()
    {
        if (Activated)
        {
            xRotation += rotationSpeed;
            transform.rotation = Quaternion.Euler(xRotation, transform.rotation.y, transform.rotation.z);
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
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
