using System.Collections;
using UnityEngine;

public class AxeDestroy : MonoBehaviour
{
    [SerializeField] private float timeToDestroyAfterCollision;
    [SerializeField] private Transform playersHand;

    [SerializeField] private  int xRotation;
    [SerializeField] private  int yRotation;
    [SerializeField] private  int zRotation;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Environment")
        {
            rigidBody.isKinematic = true;
            StartCoroutine(DestroyAxe());
        }   
    }

    private IEnumerator DestroyAxe()
    {
        yield return new WaitForSeconds(timeToDestroyAfterCollision);
        gameObject.SetActive(false);
        gameObject.transform.parent = playersHand;
        gameObject.transform.position = playersHand.position;
        //TODO после перемещения рандомные углы каждый раз
        gameObject.transform.rotation = Quaternion.Euler(xRotation,yRotation,zRotation);
        gameObject.SetActive(true);
    }
}
