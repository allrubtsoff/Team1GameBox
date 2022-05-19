public interface IDamageable 
{
    public float Hp { get; set; }
    void TakeDamage(float damage);
    void CheckDeath();
}
