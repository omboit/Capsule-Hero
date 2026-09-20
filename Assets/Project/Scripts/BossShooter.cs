using UnityEngine;

public class BossShooter : MonoBehaviour
{
    public GameObject bossBulletPrefab;
    public Transform bossFirePoint;
    public float shootInterval = 2f; // Tembak setiap 2 saat
    
    private float timer;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        if (bossBulletPrefab != null && bossFirePoint != null)
        {
            // Tembak peluru menghadap depan (sebab Boss memang dah pandang Hero)
            Instantiate(bossBulletPrefab, bossFirePoint.position, bossFirePoint.rotation);
        }
    }
}