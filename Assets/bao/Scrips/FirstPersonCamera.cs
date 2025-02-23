using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Transform của Player
    public Vector3 offset = new Vector3(0, 2, -5); // Offset từ Player
    public float smoothSpeed = 0.125f; // Tốc độ làm mịn

    void LateUpdate()
    {
        if (target == null) return; // Kiểm tra nếu target chưa được gán

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Xoay camera theo Player (nếu cần)
         transform.LookAt(target);
    }
}