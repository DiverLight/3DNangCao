using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Tham chiếu đến Settings Panel

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); // Bật/tắt panel
    }
}
