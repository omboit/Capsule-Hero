using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 3f;
    public int damage = 15; // Boss tolak 15 nyawa sekali tembak

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime); 
    }

    void OnTriggerEnter(Collider other)
    {
        // Kalau kena kawan sendiri (Virus lain), jangan buat apa-apa
        if (other.CompareTag("Enemy")) return; 

        // Kalau kena Player, tolak nyawa dia
        if (other.CompareTag("Player"))
        {
            PlayerController hero = other.GetComponent<PlayerController>();
            if (hero != null) hero.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}