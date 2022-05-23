using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp;
    [SerializeField] private LayerMask _layerMask;
    public float Hp { get; set; }

    private void Start()
    {
        Hp = hp;
    }

    public void RestoreHealth(float amount)
    {
        Hp += amount;
        Hp = Mathf.Min(Hp, hp);
    }

    public void TakeDamage(float damage, LayerMask mask)
    {
        if (_layerMask == mask)
        {
            Hp -= damage;
            CheckDeath();
        }
    }

    public void CheckDeath()
    {
        if (Hp < 1)
            Debug.Log("enemy died");
    }
}
