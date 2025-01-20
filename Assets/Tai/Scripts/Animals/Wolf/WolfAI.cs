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

    public Vector3 originalePosition; // vi tri ban dau 

    public float maxDistance = 50f; // khoang cach toi da

    Animator _anim; // khai bao component

    // state machine
    public enum CharacterState
    {
        Normal,
        Attack
    }

    public CharacterState currentState; // trang thai hien tai

    private void Start()
    {
        originalePosition = transform.position;
        navMeshAgent.SetDestination(target.position);
        _anim = GetComponent<Animator>();
    }

    private void Update()
        {   
        // khoang cach tu vi tri hien tai den vi tri ban dau
        var distanceToOriginal = Vector3.Distance(originalePosition, transform.position);
        // khoang cach tu vi tri hien tai den vi tri muv tieu
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance <= radius && distanceToOriginal <= maxDistance)
        {
            // di chuyen den muc tieu
            navMeshAgent.SetDestination(target.position);
            _anim.SetFloat("Speed", navMeshAgent.velocity.magnitude );

            distance = Vector3.Distance(target.position, transform.position);
            if(distance < 2f)
            {
                // tan cong
                ChangeState(CharacterState.Attack);
            }
        }

        if (distance > radius || distanceToOriginal > maxDistance)
        {
            // quay ve vi tri ban dau
            navMeshAgent.SetDestination(originalePosition);
            _anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);

            // chuyen sang anim Idle
            distance = Vector3.Distance(originalePosition, transform.position);
            if(distance < 1f)
            {
                _anim.SetFloat("Speed", 0);
            }
            // binh thuong
            ChangeState(CharacterState.Normal);
        }
    }

    // chuyen doi trang thai
    private void ChangeState(CharacterState newState)
    {
        // exit current state
        switch (currentState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
        }
        // enter new state
        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                _anim.SetTrigger("Attack");
                break;
        }

        // update current state
        currentState = newState;
    }
}
