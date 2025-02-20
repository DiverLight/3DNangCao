using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Tham chiếu đến Settings Panel

    void Update()
    {
        // Nhấn phím ESC: Nếu Panel đang mở thì tắt
        if (Input.GetKeyDown(KeyCode.Escape) && settingsPanel.activeSelf)
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); // Bật/tắt Panel
    }

    public void QuitGame()
    {
        // Thoát game khi build, không hoạt động trong Unity Editor
        Application.Quit();

        // Nếu chạy trong Unity Editor thì in ra thông báo (chỉ để kiểm tra)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

