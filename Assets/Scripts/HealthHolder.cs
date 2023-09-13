using UnityEngine;

public class HealthHolder : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float health;

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}