using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DeerAI : MonoBehaviour
{
    public float moveDuration = 3f;
    public float idleDuration = 3f;
    public float moveRange = 6f;

    private NavMeshAgent agent;
    private bool isMoving = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(MoveCycle());
    }

    IEnumerator MoveCycle()
    {
        while (true)
        {
            // Chọn vị trí ngẫu nhiên trong phạm vi
            Vector3 randomDestination = GetRandomNavMeshPosition();
            agent.SetDestination(randomDestination);
            isMoving = true;

            yield return new WaitForSeconds(moveDuration);

            // Dừng lại
            isMoving = false;
            agent.ResetPath();
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
}
