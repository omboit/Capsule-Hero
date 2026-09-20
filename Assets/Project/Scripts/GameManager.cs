using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    
    [Header("Challenge Timer")]
    public Text timerText;
    public float timeLimit = 300f; // 5 minit = 300 saat
    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Berhentikan masa kalau dah game over
        if (isGameOver) return;

        // Kiraan menurun
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            // Bila masa habis
            timeLimit = 0;
            UpdateTimerUI();
            ShowGameOver(); // Terus panggil Game Over!
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // Tukar nombor saat kepada format Minit:Saat (05:00)
            int minutes = Mathf.FloorToInt(timeLimit / 60F);
            int seconds = Mathf.FloorToInt(timeLimit - minutes * 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); 
            Time.timeScale = 0f; // Hentikan game
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}