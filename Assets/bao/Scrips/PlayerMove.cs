using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float boostSpeed = 1.5f;
    public KeyCode boostKey = KeyCode.LeftShift;
    public Vector3 movemnetVelocity;
    public playerinput PlayerInput;
    //public Damegezone damegezone;
    public CharacterController characterController;
    private bool isGrounded;
    //public HP_VS_MP hp_vs_mp;

    public float friction = 1.0f;
    public float rotationSmoothTime = 0.1f;
    private Vector3 movementVelocity;
    private Transform playerTransform;

    public Animator animator;
    //public AudioClip runSound; 
    //public AudioSource audioSource;

    public GameObject panel_Player;
    private bool isPanelActive = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.clip = runSound; 
    }


    void Update()
    {
        //Attack();
        MovePlayer();
        characterController.Move(movemnetVelocity);

    }

    public void MovePlayer()
    {

        movemnetVelocity.Set(PlayerInput.horizontalInput, 0, PlayerInput.veticalInput);
        movemnetVelocity.Normalize();
        movemnetVelocity = Quaternion.Euler(0, -45, 0) * movemnetVelocity;
        movemnetVelocity *= speed * Time.deltaTime;
        animator.SetFloat("Speed", movemnetVelocity.magnitude);

        if (movemnetVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movemnetVelocity);
        }
        if (Input.GetKeyDown(boostKey))
        {
            speed += boostSpeed;
            animator.SetBool("Run", true);


        }
        else if (Input.GetKeyUp(boostKey))
        {
            speed -= boostSpeed;
            animator.SetBool("Run", false);

        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            movemnetVelocity.y = jumpSpeed;
            animator.SetTrigger("MoveJump");
        }
        else if (isGrounded)
        {
            movemnetVelocity.y = Mathf.Max(movemnetVelocity.y, -2f); // Limit downward velocity when grounded
        }
        animator.SetBool("IsGrounded", isGrounded);

    }

    void FixedUpdate()
    {
        movementVelocity *= speed * Time.deltaTime;
        movementVelocity = Vector3.MoveTowards(movementVelocity, Vector3.zero, friction * Time.deltaTime);

        // Apply gravity
        movementVelocity.y += Physics.gravity.y * Time.deltaTime;

        // Update player position
        characterController.Move(movementVelocity * Time.deltaTime);

        //if (movementVelocity != Vector3.zero)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(movementVelocity);
        //    playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSmoothTime * Time.deltaTime);
        //}

        // Check if grounded
        isGrounded = characterController.isGrounded;
    }

    //public void Attack()
    //{
    //  if (Input.GetMouseButtonDown(0)) // Kiểm tra nếu chuột trái được nhấn
    //  {
    //    animator.SetTrigger("combo1");
    //  }
    //  if (Input.GetMouseButtonDown(1)) // Kiểm tra nếu chuột trái được nhấn
    //  {
    //    animator.SetTrigger("combo2");
    //  }

    //}


}