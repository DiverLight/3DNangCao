using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    protected Animator _anim; // Animator của đối tượng
    protected Transform target; // Mục tiêu (người chơi)
    public float attackRange = 1.5f; // Tầm đánh
    public int damage = 10; // Sát thương
    public float attackCooldown = 1.0f; // Thời gian hồi chiêu

    private float attackCooldownTimer; // Bộ đếm thời gian hồi chiêu

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected virtual void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (target != null && IsTargetInRange())
        {
            PerformAttack();
        }
        else
        {
            StopAttack();
        }
    }

    protected bool IsTargetInRange()
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) <= attackRange;
    }

    protected virtual void PerformAttack()
    {
        if (attackCooldownTimer <= 0)
        {
            _anim.SetTrigger("Attack");
            attackCooldownTimer = attackCooldown;

            // Gây sát thương
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    protected virtual void StopAttack()
    {
        _anim.ResetTrigger("Attack");
    }
}
