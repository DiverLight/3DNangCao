using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerinput : MonoBehaviour
{
    public bool AtkInput;
    public Animator animator; // Thêm tham chiếu đến Animator

    void Update()
    {
        if (!AtkInput && Time.timeScale != 0)
        {
            AtkInput = Input.GetMouseButtonDown(0); 
            if (AtkInput && animator != null) // Kiểm tra AtkInput và animator
            {
                animator.SetTrigger("Atk1"); // Kích hoạt animation "Attack"
            }
        }

        // Reset AtkInput sau khi đã xử lý
        if (AtkInput)
        {
            AtkInput = false;
        }
    }
}