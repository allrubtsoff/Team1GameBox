using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using System;

public class DashMove : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 10;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 2f;

    private ThirdPersonController personController;
    private StarterAssetsInputs inputs;

    private void Start()
    {
        personController = GetComponent<ThirdPersonController>();
        inputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (inputs.dash && !personController.IsDashing)
        {
            personController.IsDashing = true;
            inputs.dash = false;
            StartCoroutine(DashTimerCorutine());
        }

        if (personController.IsDashing) Dash();
    }

    private void Dash()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, personController._targetRotation, 0.0f) * Vector3.forward;
        personController.Controller.Move(targetDirection.normalized * (dashSpeed * Time.deltaTime) +
                 new Vector3(0.0f, personController._verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private IEnumerator DashTimerCorutine()
    {
        Debug.Log("DashTimer");
        yield return new WaitForSeconds(dashTime);
        personController.IsDashing = false;
    }
}
