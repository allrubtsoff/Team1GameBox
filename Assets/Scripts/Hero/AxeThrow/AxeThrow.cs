using StarterAssets;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeleeAtack))]
public class AxeThrow : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private Transform hand;
    [SerializeField] private float throwPower;
    [SerializeField] private float CoolDown;

    private AnimatorManager animatorManager;
    private MousePositionManager mouseManager;
    private StarterAssetsInputs input;
    private Vector3 throwDirection;
    private bool isAxeThrow;
    private Rigidbody axeRigidBody;

    public static event Action<float> UpdateUI;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        axeRigidBody = axe.GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();
        mouseManager = GetComponent<MeleeAtack>().GetMouseManager();
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        if (!isAxeThrow && input.throwAxe) 
        {   if (UpdateUI != null)
                UpdateUI.Invoke(CoolDown);
            ChangeState();
        }
    }

    private void ChangeState()
    {
        throwDirection = mouseManager.GetMousePosition();
        mouseManager.LookAtMouseDirection();
        animatorManager.CheckBackwardRun();
        animatorManager.SetAxeThrow(true);
        StartCoroutine(ThrowCoolDown());
    }

    private IEnumerator ThrowCoolDown()
    {
        isAxeThrow = true;
        yield return new WaitForSeconds(CoolDown);
        resetThrowAxeState();
        isAxeThrow = false;
    }

    //Called in the middle of Animation
    private void ThrowAxe()
    {
        axeRigidBody.isKinematic = false;
        axeRigidBody.transform.parent = null;
        axe.transform.LookAt(throwDirection);
        Vector3 direction = (throwDirection - axe.transform.position).normalized;
        axeRigidBody.AddForce(direction * throwPower, ForceMode.Impulse);
    }

    //Called after ThrowAxe event
    public void UpdateAxe()
    {
        resetThrowAxeState();
        mouseManager.StopLookingAtMouseDirection();
        animatorManager.ResetBackwardRun();
    }

    private void resetThrowAxeState()
    {
        input.throwAxe = false;
        animatorManager.SetAxeThrow(false);
    }
}
