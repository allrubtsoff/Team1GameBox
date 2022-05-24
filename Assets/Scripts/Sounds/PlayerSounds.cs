using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource footSteps;
    [Space(5)]
    [SerializeField] private AudioSource jumpSound;
    [Space(5)]
    [SerializeField] private AudioSource groundedSound;
    [Space(5)]
    [SerializeField] private AudioSource swingAttack;
    [Space(5)]
    [SerializeField] private AudioSource kickSound;
    [Space(5)]
    [SerializeField] private AudioSource throwSound;
    [Space(5)]
    [SerializeField] private AudioSource dashSound;
    [Space(5)]
    [SerializeField] private AudioSource airAttackSound;
    [Space(5)]
    [SerializeField] private AudioSource takeDamageSound;
    [Space(5)]
    [SerializeField] private AudioSource deathSound;
    [Space(5)]
    [SerializeField] private AudioSource axeAppearSound;
    [Space(5)]
    [SerializeField] private AudioSource lowHPSound;
    [Space(5)]
    [Header("Clips Arrays")]
    [Space(5)]
    [SerializeField] private AudioClip[] stepsArray;
    [Space(5)]
    [SerializeField] private AudioClip[] swingsArray;
    [Space(5)]
    [SerializeField] private AudioClip[] throwArray;
    [Space(5)]
    [SerializeField] private AudioClip[] axeAppearArray;
    [Space(5)]
    [SerializeField] private AudioClip[] takeDamageArray;
    [Space(5)]
    [SerializeField] private AudioClip[] dashArray;
    [Space(5)]
    [SerializeField] private AudioClip[] groundedArray;

    public void PlayLowHPSound()
    {
        lowHPSound.Play();
    }
    public void PlayAirAttackSound()
    {
        airAttackSound.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void PlayKickSound()
    {
        kickSound.Play();
    }

    public void PlayFootStepsSound()
    {
        int r = Random.Range(0, stepsArray.Length);
        footSteps.clip = stepsArray[r];
        footSteps.Play();
    }
    public void PlaySwordSound()
    {
        int r = Random.Range(0, swingsArray.Length);
        swingAttack.clip = swingsArray[r];
        swingAttack.Play();
    }

    public void PlayThrowSound()
    {
        int r = Random.Range(0, throwArray.Length);
        throwSound.clip = throwArray[r];
        throwSound.Play();
    }
    public void PlayAxeAppearSound()
    {
        int r = Random.Range(0, axeAppearArray.Length);
        axeAppearSound.clip = axeAppearArray[r];
        axeAppearSound.Play();
    }
    public void PlayDashSound()
    {
        int r = Random.Range(0, dashArray.Length);
        dashSound.clip = dashArray[r];
        dashSound.Play();
    }
    public void PlayGroundedSound()
    {
        int r = Random.Range(0, groundedArray.Length);
        groundedSound.clip = groundedArray[r];
        groundedSound.Play();
    }

}
