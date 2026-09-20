using UnityEngine;
using UnityEngine.SceneManagement; // Ini wajib untuk sistem tukar map

public class LevelPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Semak adakah yang langgar portal ni adalah Hero kita
        if (other.CompareTag("Player"))
        {
            // Dapatkan nombor turutan map sekarang, dan tambah 1
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // Pastikan map seterusnya wujud dalam senarai Build Settings
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Pindah ke map nombor: " + nextSceneIndex);
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                // Kalau takde map seterusnya (dah habis Level 3)
                Debug.Log("Tahniah! Ini level terakhir!");
                // Nanti kita boleh tukar ke skrin Menang (You Win) di sini
            }
        }
    }
}