using UnityEngine;
using TMPro; // Guna UnityEngine.UI; kalau pakai Text biasa

public class SkrinMenang : MonoBehaviour
{
    public TMP_Text teksTunjukMarkah; 

    void Start()
    {
        // Panggil balik markah terkumpul dari memori
        int markahTotal = PlayerPrefs.GetInt("MarkahPemain", 0);
        
        if (teksTunjukMarkah != null)
        {
            teksTunjukMarkah.text = "MARKAH KESELURUHAN: " + markahTotal.ToString();
        }

        // Buka cursor untuk pemain tekan butang "Keluar" (jika ada)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Reset markah ke kosong (supaya kalau mula game baru, markah kembali 0)
        PlayerPrefs.SetInt("MarkahPemain", 0);
    }
}