using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeThrow : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private Transform hand;
    [SerializeField] private float throwPower;
    [SerializeField] private float CoolDown;


    private StarterAssetsInputs input;
    private Animator animator;
    private Rigidbody axeRigidBody;
    private AxeRotate axeRotate;

    private bool isAxeThrow;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        axeRigidBody = axe.GetComponent<Rigidbody>();
        axeRotate = axe.GetComponent<AxeRotate>();
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (!isAxeThrow)
        {
            //Called when R key is hold
            if (input.throwAxe)
                ChangeState(true);
            //Called when R key is pressed
            else if (input.isThrowAxePressed)
                ChangeState(false);
        }
    }

    private void ChangeState(bool isHold)
    {
        if (isHold)
        {
            animator.SetBool("AxeAim", true);
        }
        else
        {
            animator.SetBool("AxeThrow", true);
        }
        StartCoroutine(ThrowCoolDown());
    }

    private IEnumerator ThrowCoolDown()
    {
        isAxeThrow = true;
        yield return new WaitForSeconds(CoolDown);
        isAxeThrow = false;
    }

    //Called in the middle of Animation
    private void ThrowAxe()
    {
        axeRigidBody.isKinematic = false;
        axeRigidBody.transform.parent = null;
        axeRigidBody.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        axeRotate.Activated = true;
    }

    //Called in the start of Animation
    private void resetThrowAxeState()
    {
        input.isThrowAxePressed = false;
        input.throwAxe = false;
        animator.SetBool("AxeThrow", false);
        animator.SetBool("AxeAim", false);
    }
}
