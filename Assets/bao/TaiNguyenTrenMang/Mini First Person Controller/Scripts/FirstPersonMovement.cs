using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public KeyCode jumpKey = KeyCode.Space;

    Rigidbody rigidbody;
    public Animator animator;
    public GroundCheck groundCheck;
    public AudioSource runAudioSource; // AudioSource cho âm thanh chạy
    public AudioSource jumpAudioSource; // AudioSource cho âm thanh nhảy
    public AudioClip runSound; // Âm thanh chạy
    public AudioClip jumpSound; // Âm thanh nhảy

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (groundCheck != null && groundCheck.isGrounded && Input.GetKeyDown(jumpKey))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (animator != null)
            {
                animator.SetBool("IsJumping", !groundCheck.isGrounded);
            }
            if (jumpAudioSource != null && jumpSound != null)
            {
                jumpAudioSource.PlayOneShot(jumpSound); // Phát âm thanh nhảy
            }
        }
    }

    void FixedUpdate()
    {
        IsRunning = canRun && Input.GetKey(runningKey);

        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);

        if (animator != null)
        {
            if (targetVelocity != Vector2.zero)
            {
                animator.SetFloat("Speed", targetVelocity.magnitude);
                // Phát âm thanh chạy khi di chuyển
                if (IsRunning && groundCheck.isGrounded && runAudioSource != null && runSound != null && !runAudioSource.isPlaying)
                {
                    runAudioSource.clip = runSound;
                    runAudioSource.loop = true;
                    runAudioSource.Play();
                }
                else if (!IsRunning || !groundCheck.isGrounded || runAudioSource == null || runSound == null)
                {
                    runAudioSource.loop = false;
                    runAudioSource.Stop();
                }
            }
            else
            {
                animator.SetFloat("Speed", 0);
                if (runAudioSource != null)
                {
                    runAudioSource.loop = false;
                    runAudioSource.Stop();
                }
            }
        }

        if (groundCheck != null)
        {
            animator.SetBool("IsJumping", groundCheck.isGrounded);
        }
    }
}