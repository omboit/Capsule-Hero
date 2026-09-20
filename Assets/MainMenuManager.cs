using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Call this method from the Credits Button OnClick event
    public void LoadCreditsScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}