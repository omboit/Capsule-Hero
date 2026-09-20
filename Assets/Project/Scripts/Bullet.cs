using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 1;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime); 
    }

    // FUNGSI BARU: OnTriggerEnter untuk peluru jenis laser/tenaga
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return; 
        }

        if (other.CompareTag("Enemy"))
        {
            EnemyVirus virus = other.GetComponent<EnemyVirus>();
            if (virus != null) virus.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}