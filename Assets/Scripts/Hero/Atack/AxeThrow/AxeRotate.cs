using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    public bool Activated;

    void Update()
    {
        if (Activated)
            transform.localEulerAngles += transform.forward * rotationSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Lol");
        Activated = false;
        //GetComponent<Rigidbody>().isKinematic = true;
    }
}
