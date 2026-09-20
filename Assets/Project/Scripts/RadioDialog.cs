using UnityEngine;
using UnityEngine.UI;
using TMPro; // TAMBAHAN WAJIB: Untuk sokong sistem teks baru Unity

public class RadioDialog : MonoBehaviour
{
    [Header("Maklumat Dialog")]
    public string namaPenyampai = "SISTEM AI";
    [TextArea(3, 5)]
    public string ayatDialog;

    [Header("Sambungan UI")]
    public GameObject kotakDialogUI; 
    
    // TUKAR KE TextMeshProUGUI (atau TMP_Text)
    public TMP_Text teksNamaUI;          
    public TMP_Text teksMesejUI;         

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (kotakDialogUI != null) kotakDialogUI.SetActive(true);
            
            if (teksNamaUI != null) teksNamaUI.text = namaPenyampai;
            if (teksMesejUI != null) teksMesejUI.text = ayatDialog;

            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null) audio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (kotakDialogUI != null) kotakDialogUI.SetActive(false);
        }
    }
}