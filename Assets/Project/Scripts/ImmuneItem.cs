using UnityEngine;

public class ImmuneItem : MonoBehaviour
{
    public float immuneDuration = 10f; // Berapa lama kebal (10 saat)

    void OnTriggerEnter(Collider other)
    {
        // Semak kalau yang langgar tu adalah Player
        if (other.CompareTag("Player"))
        {
            PlayerController hero = other.GetComponent<PlayerController>();
            if (hero != null)
            {
                hero.ActivateImmunity(immuneDuration); // Panggil fungsi kebal
                Destroy(gameObject); // Hilangkan bola lepas dikutip
            }
        }
    }
}