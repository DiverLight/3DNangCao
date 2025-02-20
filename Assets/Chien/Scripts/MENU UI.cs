using UnityEngine;
using UnityEngine.SceneManagement;

public class MENUUI : MonoBehaviour
{
    public void PlayGame()  // Hàm phải là public
    {
        SceneManager.LoadScene("Map 1"); // Thay "Map 1" bằng đúng tên scene
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
