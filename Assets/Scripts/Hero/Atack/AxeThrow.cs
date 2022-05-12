using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    [SerializeField] private Rigidbody axeRigidBody;
    [SerializeField] private float throwPower;

    private StarterAssetsInputs input;
    private Animator animator;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(input.throwAxe)
        {
            animator.SetBool("AxeThrow", true);
        }
    }
    
    private void ThrowAxe()
    {
        axeRigidBody.isKinematic = false;
        axeRigidBody.transform.parent = null;
        axeRigidBody.AddForce(transform.forward * throwPower, ForceMode.Impulse);
    }

    public void resetThrowAxeState()
    {
        input.throwAxe = false;
        animator.SetBool("AxeThrow", false);
    }
}
