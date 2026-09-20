using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    [Header("Virus Prefabs")]
    public GameObject greenVirusPrefab;
    public GameObject redVirusPrefab;
    public GameObject kingVirusPrefab;

    [Header("Spawner Settings")]
    public Transform[] spawnPoints; 
    public float spawnInterval = 3f; 

    [Header("Wave Settings (Masa)")]
    public float timeToLevel2 = 30f; // Selepas 30 saat, Red Virus keluar
    public float timeToBoss = 60f;   // Selepas 60 saat, King Virus keluar

    private float gameTimer = 0f;
    private float spawnTimer = 0f;
    private bool bossSpawned = false;

    void Update()
    {
        // Kira masa game berjalan
        gameTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        // Bila masa dah cukup, keluarkan virus
        if (spawnTimer >= spawnInterval)
        {
            SpawnVirus();
            spawnTimer = 0f; 
        }
    }

    void SpawnVirus()
    {
        if (spawnPoints.Length == 0) return;

        // Pilih lokasi rawak
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomPoint = spawnPoints[randomIndex];

        // LOGIK GELOMBANG (WAVE SYSTEM)
        if (gameTimer >= timeToBoss && !bossSpawned)
        {
            // FASA 3: Boss Spawn (Sekali sahaja)
            Instantiate(kingVirusPrefab, randomPoint.position, randomPoint.rotation);
            bossSpawned = true;
            Debug.Log("AMARAN: KING VIRUS MUNCUL!");
        }
        else if (gameTimer >= timeToLevel2 && gameTimer < timeToBoss)
        {
            // FASA 2: Random keluar Hijau (50%) atau Merah (50%)
            int randomChance = Random.Range(0, 2); // Nombor 0 atau 1
            GameObject virusToSpawn = (randomChance == 0) ? greenVirusPrefab : redVirusPrefab;
            
            Instantiate(virusToSpawn, randomPoint.position, randomPoint.rotation);
        }
        else if (gameTimer < timeToLevel2)
        {
            // FASA 1: Hanya Virus Hijau
            Instantiate(greenVirusPrefab, randomPoint.position, randomPoint.rotation);
        }
    }
}