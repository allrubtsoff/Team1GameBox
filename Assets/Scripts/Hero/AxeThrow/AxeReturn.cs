using System.Collections;
using UnityEngine;

public class AxeReturn: MonoBehaviour
{
    [SerializeField] private float timeToRelocateAfterCollision;
    [SerializeField] private Transform playersHand;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            rigidBody.isKinematic = true;
            StartCoroutine(ReturnAxe());
        }
    }

    private IEnumerator ReturnAxe()
    {
        yield return new WaitForSeconds(timeToRelocateAfterCollision);
        gameObject.SetActive(false);
        gameObject.transform.parent = playersHand;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
