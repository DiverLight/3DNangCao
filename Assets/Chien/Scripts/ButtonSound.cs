using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Chứa âm thanh
    public AudioClip buttonClickSound; // File âm thanh khi nhấn nút

    public void PlayButtonClick()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
