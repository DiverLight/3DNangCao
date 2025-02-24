using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Transform leftDoor;  // Cánh cửa trái
    public Transform rightDoor; // Cánh cửa phải
    private bool isOpen = false;
    private float speed = 3f;

    private Quaternion leftClosedRotation, leftOpenRotation;
    private Quaternion rightClosedRotation, rightOpenRotation;

    void Start()
    {
        // Lưu trạng thái đóng ban đầu của 2 cửa
        leftClosedRotation = leftDoor.rotation;
        rightClosedRotation = rightDoor.rotation;

        // Cửa mở 90 độ theo hướng tương ứng
        leftOpenRotation = Quaternion.Euler(0, leftDoor.eulerAngles.y - 90, 0);
        rightOpenRotation = Quaternion.Euler(0, rightDoor.eulerAngles.y + 90, 0);
    }

    void Update()
    {
        if (IsPlayerNearby())
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }

        // Xoay cửa theo trạng thái mở/đóng
        leftDoor.rotation = Quaternion.Lerp(leftDoor.rotation, isOpen ? leftOpenRotation : leftClosedRotation, Time.deltaTime * speed);
        rightDoor.rotation = Quaternion.Lerp(rightDoor.rotation, isOpen ? rightOpenRotation : rightClosedRotation, Time.deltaTime * speed);
    }

    private bool IsPlayerNearby()
    {
        return Vector3.Distance(transform.position, Camera.main.transform.position) < 3f;
    }
}
