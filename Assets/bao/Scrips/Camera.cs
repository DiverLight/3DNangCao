using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Atttribute Camera")]
    public Transform Targer;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.7f, -4f);
    private Quaternion rotation;

    private float x;
    private float y;

    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float ySpeed = 4f;

    [SerializeField] private float xMinRotation = -360f;
    [SerializeField] private float xMaxRotation = 360f;
    [SerializeField] private float yMinRotation = 10f;
    [SerializeField] private float yMaxRotation = 80f;

    private bool isLocked = false; // Flag to track lock state
    internal float fieldOfView;
    internal static object main;

    void Start()
    {
        Vector3 angles = this.transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            isLocked = !isLocked; // Toggle lock state
        }
    }

    public void LateUpdate()
    {
        if (!isLocked)
        {
            CameraMove();
        }

        rotation = Quaternion.Euler(y, x, 0);
        Vector3 distanceVector = offset;
        Vector3 position = rotation * distanceVector + Targer.position;
        transform.rotation = rotation;
        transform.position = position;
    }

    public void CameraMove()
    {
        x += Input.GetAxis("Mouse X") * xSpeed;
        y += Input.GetAxis("Mouse Y") * ySpeed;

        x = ClampAngle(x, xMinRotation, xMaxRotation);
        y = ClampAngle(y, yMinRotation, yMaxRotation);
    }

    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }
}