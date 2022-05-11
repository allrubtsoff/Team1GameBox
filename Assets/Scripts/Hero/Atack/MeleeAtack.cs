using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    private StarterAssetsInputs input;
    private Animator animator;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Atack();
    }

    private void Atack()
    {
        if (input.atack)
        {
            animator.SetBool("Atack", true);
            input.atack = false;
        }
        else
            animator.SetBool("Atack", false);
    }
}
