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
    private bool isAxeThrow;

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
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
            animatorManager.SetAxeAim(true);
        }
        else
        {
            Vector3 direction = mouseManager.MousePosition - transform.position;
            Debug.DrawRay(transform.position, direction, Color.blue);
            animatorManager.SetAxeThrow(true);
            StartCoroutine(ThrowCoolDown());
        }
    }

    private IEnumerator ThrowCoolDown()
    {
        isAxeThrow = true;
        yield return new WaitForSeconds(CoolDown);
        isAxeThrow = false;
    }

    //Called after ThrowAxe event
    public void UpdateAxe()
    {
        this.axe = Instantiate(axe, hand);
        axe.transform.position = hand.position;
    }

    //Called in the middle of Animation
    private void ThrowAxe()
    {
        var axeRigidBody = axe.GetComponent<Rigidbody>();
        axeRigidBody.isKinematic = false;
        axeRigidBody.transform.parent = null;
        Vector3 direction = (mouseManager.MousePosition-transform.position).normalized;
        Debug.DrawRay(transform.position, direction, Color.blue, 10f);
        axeRigidBody.AddForce(direction * throwPower,ForceMode.Impulse);
        axeRigidBody.GetComponent<AxeRotate>().Activated = true;
    }

    //Called in the start of Animation
    private void resetThrowAxeState()
    {
        input.isThrowAxePressed = false;
        input.throwAxe = false;
        animatorManager.SetAxeAim(false);
        animatorManager.SetAxeThrow(false);
    }
}
