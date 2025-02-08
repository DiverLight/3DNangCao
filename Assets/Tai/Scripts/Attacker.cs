using UnityEngine;

public class Attacker : MonoBehaviour
{
    public int damage = 20;
    public float attackCooldown = 1.5f;

    private float lastAttackTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null && Time.time >= lastAttackTime + attackCooldown)
        {
            target.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
