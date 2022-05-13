using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

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
        Activated = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
