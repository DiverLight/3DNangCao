using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Inventory inventory;
    public float movementSpeed = 5f; // Tốc độ di chuyển của nhân vật
    public float rotationSpeed = 100f; // Tốc độ xoay của nhân vật

    void Update()
    {
        // Di chuyển
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        if (movement.magnitude > 0) // Kiểm tra xem có input di chuyển không
        {
            // Xử lý xoay nhân vật theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Di chuyển nhân vật
            transform.Translate(movement.normalized * movementSpeed * Time.deltaTime, Space.World);
        }

    }

   
}