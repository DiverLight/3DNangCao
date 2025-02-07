using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SheepAI : MonoBehaviour
{
    // Thời gian di chuyển trước khi dừng lại
    public float moveDuration = 3f;
    // Thời gian nghỉ trước khi di chuyển tiếp
    public float idleDuration = 3f;
    // Phạm vi di chuyển ngẫu nhiên
    public float moveRange = 8f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isMoving = false;

    void Start()
    {
        // Lấy component NavMeshAgent và Animator
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Bắt đầu vòng lặp di chuyển
        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            // Chọn vị trí ngẫu nhiên trong phạm vi trên NavMesh
            Vector3 randomDestination = GetRandomNavMeshPosition();
            agent.SetDestination(randomDestination);
            isMoving = true;
            animator.SetBool("isMoving", true);

            // Chờ trong khoảng thời gian di chuyển
            yield return new WaitForSeconds(moveDuration);

            // Dừng lại và reset đường đi
            isMoving = false;
            agent.ResetPath();
            animator.SetBool("isMoving", false);

            // Chờ trong khoảng thời gian nghỉ
            yield return new WaitForSeconds(idleDuration);
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        // Tạo một hướng ngẫu nhiên trong phạm vi
        Vector3 randomDirection = Random.insideUnitSphere * moveRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        // Lấy vị trí hợp lệ gần nhất trên NavMesh
        NavMesh.SamplePosition(randomDirection, out hit, moveRange, NavMesh.AllAreas);
        return hit.position;
    }
}
