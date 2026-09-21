using UnityEngine;
using UnityEngine.UI; // Wajib guna ini untuk Text biasa

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    // Ini yang dah ditukar, dari TMP_Text kepada Text
    public Text scoreText; 
    
    private int score = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Ambil balik markah dari memori
        score = PlayerPrefs.GetInt("MarkahPemain", 0);
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
        
        // HAFAL MARKAH TERKINI
        PlayerPrefs.SetInt("MarkahPemain", score); 
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "MARKAH: " + score;
        }
    }
}