using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameOnly : MonoBehaviour
{
    // Loads the scene named "Tutorial" (or whatever scene name you set in Inspector)
    public void LoadGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
}