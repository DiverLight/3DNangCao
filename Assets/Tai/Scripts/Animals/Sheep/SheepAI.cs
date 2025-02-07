using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SheepAI : MonoBehaviour, IDamageable
{
    public float moveDuration = 3f;
    public float idleDuration = 3f;
    public float moveRange = 8f;

    public int maxHealth = 100;
    private int currentHealth;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isMoving = false;

    public Slider healthBar;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        UpdateHealthUI();

        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            Vector3 randomDestination = GetRandomNavMeshPosition();
            agent.SetDestination(randomDestination);
            isMoving = true;
            animator.SetBool("isMoving", true);

            yield return new WaitForSeconds(moveDuration);

            isMoving = false;
            agent.ResetPath();
            animator.SetBool("isMoving", false);

            yield return new WaitForSeconds(idleDuration);
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, moveRange, NavMesh.AllAreas);
        return hit.position;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        agent.enabled = false;
        this.enabled = false;
    }
}
