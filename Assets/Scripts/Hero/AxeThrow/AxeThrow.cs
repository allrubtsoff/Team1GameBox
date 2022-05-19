using StarterAssets;
using System.Collections;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private Transform hand;
    [SerializeField] private float throwPower;
    [SerializeField] private float CoolDown;
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private MousePositionManager mouseManager;


    private StarterAssetsInputs input;
    private Vector3 throwDirection;
    private bool isAxeThrow;
    private Rigidbody axeRigidBody;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        axeRigidBody = axe.GetComponent<Rigidbody>();
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
        throwDirection = mouseManager.MousePosition;
        mouseManager.LookAtMouseDirection();
        animatorManager.CheckBackwardRun();

        if (isHold)
        {
            animatorManager.SetAxeAim(true);
        }
        else
        {
            animatorManager.SetAxeThrow(true);
            StartCoroutine(ThrowCoolDown());
        }
    }

    private IEnumerator ThrowCoolDown()
    {
        isAxeThrow = true;
        yield return new WaitForSeconds(CoolDown);
        resetThrowAxeState();
        isAxeThrow = false;
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
        input.isThrowAxePressed = false;
        input.throwAxe = false;
        animatorManager.SetAxeAim(false);
        animatorManager.SetAxeThrow(false);
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
}
