using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedSpawner : MonoBehaviour
{
    public bool canSpawn = true;
    public GameObject tumbleweedPrefab;
    public List<Transform> tumbleweedSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns ;
    private List<GameObject> tumbleweedsList = new List<GameObject>();

    private void SpawnTumbleweed()
    {
        Vector3 randomPosition = tumbleweedSpawnPositions[Random.Range(0, tumbleweedSpawnPositions.Count)].position;
        GameObject tumbleweed = Instantiate(tumbleweedPrefab, randomPosition ,tumbleweedPrefab.transform.rotation);
        tumbleweedsList.Add(tumbleweed);
        tumbleweed.GetComponent<Tumbleweed>().SetSpawner(this);
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnTumbleweed();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveTumbleweedFromList (GameObject tumbleweed)
    {
    tumbleweedsList.Remove(tumbleweed);
    }

        void Start()
    {
        StartCoroutine (SpawnRoutine());
    }

    void Update()
    {
        
    }
}
