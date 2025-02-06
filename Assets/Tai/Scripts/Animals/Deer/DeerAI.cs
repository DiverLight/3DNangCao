using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeerAI : MonoBehaviour
{
    // Thời gian di chuyển trước khi dừng lại
    public float moveDuration = 3f;
    // Thời gian nghỉ trước khi di chuyển tiếp
    public float idleDuration = 3f;
    // Phạm vi di chuyển ngẫu nhiên
    public float moveRange = 6f;

    private NavMeshAgent agent; // Component NavMeshAgent để điều khiển di chuyển của đối tượng
    private Animator animator; // Component Animator để điều khiển animation của đối tượng
    private bool isMoving = false; // Biến đánh dấu trạng thái di chuyển

    void Start()
    {
        // Lấy component NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        // Lấy component Animator
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
            // Đặt mục tiêu di chuyển tới vị trí ngẫu nhiên
            agent.SetDestination(randomDestination);
            // Bắt đầu di chuyển
            isMoving = true;
            // Kích hoạt animation "isWalking"
            animator.SetBool("isWalking", true);

            // Chờ trong khoảng thời gian di chuyển
            yield return new WaitForSeconds(moveDuration);

            // Dừng di chuyển
            isMoving = false;
            // Reset đường đi của NavMeshAgent
            agent.ResetPath();
            // Tắt animation "isWalking"
            animator.SetBool("isWalking", false);

            // Chờ trong khoảng thời gian nghỉ
            yield return new WaitForSeconds(idleDuration);
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        // Tạo một hướng ngẫu nhiên trong phạm vi
        Vector3 randomDirection = Random.insideUnitSphere * moveRange;
        // Thêm vị trí hiện tại để có được vị trí ngẫu nhiên trong phạm vi di chuyển
        randomDirection += transform.position;
        NavMeshHit hit;
        // Lấy vị trí hợp lệ gần nhất trên NavMesh
        NavMesh.SamplePosition(randomDirection, out hit, moveRange, NavMesh.AllAreas);
        return hit.position;
    }
}
