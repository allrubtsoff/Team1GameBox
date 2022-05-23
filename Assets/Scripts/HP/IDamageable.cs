using UnityEngine;

public interface IDamageable 
{
    public float Hp { get; set; }
    void TakeDamage(float damage, LayerMask mask);
    void CheckDeath();
}
