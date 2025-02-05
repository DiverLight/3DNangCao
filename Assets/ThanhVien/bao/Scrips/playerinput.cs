using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinput : MonoBehaviour
{
    public float horizontalInput;
    public float veticalInput;
    public bool AtkInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        veticalInput = Input.GetAxisRaw("Vertical");
        if (!AtkInput && Time.timeScale != 0)
        {
            AtkInput = Input.GetMouseButtonDown(0);// nhan chuot trai
        }
        
        
    }
    private void OnDisable()
    {
        horizontalInput = 0;
        veticalInput = 0;
        AtkInput = false;   
    }
}
