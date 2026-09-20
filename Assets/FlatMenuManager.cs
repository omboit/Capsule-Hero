using UnityEngine;
using UnityEngine.SceneManagement;

public class FlatMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Gameplay Scene Target")]
    public string gameplaySceneName = "Level_01";

    // --- Main Panel Functions ---
    public void PlayGame()
    {
        // Loads the gameplay scene
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenSettings()
    {
        // Hide main menu, show settings panel
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        // Hide main menu, show credits panel
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        // Quits the game (works in built executable)
        Debug.Log("Quit Game Executed");
        Application.Quit();
    }

    // --- Back Buttons (Inside Settings & Credits) ---
    public void BackToMain()
    {
        // Hide sub-panels, show main panel
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }
}