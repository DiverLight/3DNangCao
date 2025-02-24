using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour
{
    public Slider volumeSlider;  // Kéo thả Slider từ Inspector vào đây
    private AudioSource musicSource;

    void Start()
    {
        // Tìm AudioSource trong GameObject này
        musicSource = GetComponent<AudioSource>();

        // Gán giá trị mặc định từ PlayerPrefs nếu có
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSource.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            musicSource.volume = 1f;  // Giá trị mặc định là 100%
            volumeSlider.value = 1f;
        }

        // Gán sự kiện khi thay đổi Slider
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        musicSource.volume = volume;  // Cập nhật âm lượng
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Lưu lại giá trị âm lượng
        PlayerPrefs.Save();
    }
}