using UnityEngine;
using UnityEngine.SceneManagement; // Import SceneManager

public class MiniSettingsManager : MonoBehaviour
{
    public GameObject miniSettingsPanel;

    void Start()
    {
        // Ẩn panel lúc đầu
        miniSettingsPanel.SetActive(false);
    }

    void Update()
    {
        // Nhấn ESC để mở hoặc đóng MiniSettingsPanel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMiniSettings();
        }
    }

    public void ToggleMiniSettings()
    {
        bool isActive = !miniSettingsPanel.activeSelf;
        miniSettingsPanel.SetActive(isActive);

        // Dừng game khi mở panel
        Time.timeScale = isActive ? 0f : 1f;
    }

    public void ReturnToGame()
    {
        miniSettingsPanel.SetActive(false); // Ẩn panel
        Time.timeScale = 1f; // Tiếp tục game
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian chạy bình thường trước khi load scene mới
        SceneManager.LoadScene("Menu"); // Thay "Menu" bằng tên Scene Menu của bạn
    }
}
