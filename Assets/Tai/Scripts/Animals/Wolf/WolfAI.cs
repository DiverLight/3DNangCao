using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WolfAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform target; // Muc tieu 

    public float radius = 10f; // ban kinh tim kiem ke thu

        private void Update()
        {   
        var distance = Vector3.Distance(transform.position, target.position);

        if (distance < radius)
        {
            // di chuyen den muc tieu
            navMeshAgent.SetDestination(target.position);
        }
    }
}
