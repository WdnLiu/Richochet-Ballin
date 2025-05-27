using System.Collections;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public GameObject[] powerUpPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 15f;

    private GameObject currentPowerUp;

    public bool canSpawn;

    void Start()
    {
        Debug.Log("PowerUpLogic started!");
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => currentPowerUp == null);

            float randomOffset = Random.Range(-2f, 2f);
            yield return new WaitForSeconds(spawnInterval + randomOffset);

            if (!currentPowerUp && canSpawn)
            {
                SpawnPowerUp();
            }
        }
    }

    public void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Missing power-ups or spawn points!");
            return;
        }

        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        currentPowerUp = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        PowerUp powerUpScript = currentPowerUp.GetComponent<PowerUp>();
        if (powerUpScript != null)
        {
            powerUpScript.onCollected += HandlePowerUpCollected;
        }
        else
        {
            Debug.LogError("PowerUp script not found on: " + currentPowerUp.name);
        }
    }

    public void HandlePowerUpCollected()
    {
        if (currentPowerUp)
            Destroy(currentPowerUp);
        currentPowerUp = null;
    }
}
