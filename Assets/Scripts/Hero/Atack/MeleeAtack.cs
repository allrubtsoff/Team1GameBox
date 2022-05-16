using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(StarterAssetsInputs))]

public class MeleeAtack : MonoBehaviour
{
    private StarterAssetsInputs input;
    private Animator animator;
    [SerializeField] private AudioClip[] SwordAttackClips;
    [Range(0, 1)]  public float SwordAttackVolume = 0.5f;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        input = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Atack();
      

    }

    private void OnSwordAttack(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (SwordAttackClips.Length > 0)
            {
                var index = Random.Range(0, SwordAttackClips.Length);
                AudioSource.PlayClipAtPoint(SwordAttackClips[index], transform.TransformPoint(controller.center), SwordAttackVolume);
            }
        }
    }

    private void Atack()
    {
        if (input.atack)
        {
            animator.SetBool("Atack", true);
     
        }
    }

    //to reset state in first frame of Atack animation by AnimationEvent
    public void resetAtackState()
    {
        input.atack = false;
        animator.SetBool("Atack", false);
    }
}
 