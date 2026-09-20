using UnityEngine;
using UnityEngine.AI;

public class EnemyVirus : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public int attackDamage = 20;
    public int scoreValue = 10; // Markah untuk virus ni
    
    public GameObject explosionEffect; // Tempat letak efek letupan

    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            agent.SetDestination(player.position);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController hero = collision.gameObject.GetComponent<PlayerController>();
            if (hero != null)
            {
                hero.TakeDamage(attackDamage);
            }
        }
    }

    void Die()
    {
        // 1. Keluarkan efek letupan (Particle System)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 2. Tambah markah ke sistem UI
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
        }

        // 3. Hancurkan virus
        Destroy(gameObject);
    }
}