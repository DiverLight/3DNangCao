using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject shaderPanel;
    public GameObject keysPanel;

    void Start()
    {
        // Đảm bảo ShaderPanel và KeysPanel ẩn lúc đầu
        shaderPanel.SetActive(false);
        keysPanel.SetActive(false);
    }

    public void ShowShaderPanel()
    {
        shaderPanel.SetActive(true);
        keysPanel.SetActive(false);
    }

    public void ShowKeysPanel()
    {
        shaderPanel.SetActive(false);
        keysPanel.SetActive(true);
    }
}
