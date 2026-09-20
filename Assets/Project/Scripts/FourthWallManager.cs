using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.UI;

public class FourthWallManager : MonoBehaviour
{
    [Header("Fourth Wall Settings")]
    public Text secretMessageText; // Teks yang akan muncul bila gambar dijumpai
    
    private string folderPath;
    private bool isChecking = true;

    void Start()
    {
        // 1. Dapatkan laluan ke Desktop komputer pemain
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        folderPath = Path.Combine(desktopPath, "CapsuleHero_Evidence");

        // 2. Auto-generate folder kalau belum ada
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log("Folder rahsia dicipta di: " + folderPath);
        }

        // Sembunyikan teks mesej pada awal game
        if (secretMessageText != null) secretMessageText.gameObject.SetActive(false);

        // Mula intip folder setiap 2 saat
        StartCoroutine(MonitorFolder());
    }

    IEnumerator MonitorFolder()
    {
        while (isChecking)
        {
            if (Directory.Exists(folderPath))
            {
                // Ambil senarai fail dalam folder tersebut
                string[] files = Directory.GetFiles(folderPath); 
                
                bool imageFound = false;
                foreach (string file in files)
                {
                    // Kalau terjumpa fail jenis gambar
                    if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"))
                    {
                        imageFound = true;
                        break;
                    }
                }

                // 3. Kalau gambar dijumpai, buat kejutan!
                if (imageFound)
                {
                    TriggerSecretEvent();
                    isChecking = false; // Berhenti intip lepas berjaya
                }
            }
            yield return new WaitForSeconds(2f); // Rehat 2 saat sebelum intip lagi
        }
    }

    void TriggerSecretEvent()
    {
        Debug.Log("GAMBAR BUKTI DIKESAN!");
        if (secretMessageText != null)
        {
            secretMessageText.gameObject.SetActive(true);
            secretMessageText.text = "SYSTEM HACKED: \nANALISIS BOSS SELESAI.\nKELEMAHAN KING VIRUS ADALAH SERANGAN BERTUBI-TUBI!";
        }
    }
}
