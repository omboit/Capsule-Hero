using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main menu"); // Replace with your main menu scene name
    }
}