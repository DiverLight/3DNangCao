using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float speed = 3f;
    public float openAngle = 90f; // Góc mở cửa
    public float activationDistance = 3f; // Khoảng cách kích hoạt

    private bool isOpen = false;
    private Quaternion leftClosedRotation, leftOpenRotation;
    private Quaternion rightClosedRotation, rightOpenRotation;

    void Start()
    {
        leftClosedRotation = leftDoor.rotation;
        rightClosedRotation = rightDoor.rotation;

        leftOpenRotation = Quaternion.Euler(0, leftDoor.eulerAngles.y - openAngle, 0);
        rightOpenRotation = Quaternion.Euler(0, rightDoor.eulerAngles.y + openAngle, 0);
    }

    void Update()
    {
        isOpen = IsPlayerNearby();

        leftDoor.rotation = Quaternion.Lerp(leftDoor.rotation, isOpen ? leftOpenRotation : leftClosedRotation, Time.deltaTime * speed);
        rightDoor.rotation = Quaternion.Lerp(rightDoor.rotation, isOpen ? rightOpenRotation : rightClosedRotation, Time.deltaTime * speed);
    }

    private bool IsPlayerNearby()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, activationDistance);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player")) // Kiểm tra tag "Player"
            {
                return true;
            }
        }
        return false;
    }

    //Vẽ một hình cầu để debug trong scene view.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}