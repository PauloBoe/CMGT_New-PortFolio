using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // String voor de huidige scene naam
    public string sceneName;

    public void LoadScene()
    {
        // Zet het spel op pause en laad een andere scene
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
