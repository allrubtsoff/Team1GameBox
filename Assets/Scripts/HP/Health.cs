using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp;
    public float Hp { get; set; }

    private void Start()
    {
        Hp = hp;
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if (Hp < 1)
            Debug.Log("enemy died");
    }
}
