using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float speed = 3f;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
    }

    void Update()
    {
        isOpen = IsPlayerNearby(); // Tự động mở khi nhân vật lại gần

        transform.rotation = Quaternion.Lerp(transform.rotation, isOpen ? openRotation : closedRotation, Time.deltaTime * speed);
    }

    private bool IsPlayerNearby()
    {
        return Vector3.Distance(transform.position, Camera.main.transform.position) < 3f;
    }
}
