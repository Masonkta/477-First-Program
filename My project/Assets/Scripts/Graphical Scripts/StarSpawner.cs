using System.Collections;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab; 
    public Vector2 minSpawnPos, maxSpawnPos; 
    public float minSpawnInterval;
    public float maxSpawnInterval; 
    public float minScale; 
    public float maxScale;

    void Start()
    {
        StartCoroutine(SpawnStar());
    }

    IEnumerator SpawnStar()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval + 1));
            Vector2 spawnPos = new Vector2(
                Random.Range(minSpawnPos.x, maxSpawnPos.x),
                Random.Range(minSpawnPos.y, maxSpawnPos.y)
            );

            GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);
            float randomScale = Random.Range(minScale, maxScale);
            star.transform.localScale = new Vector3(randomScale, randomScale, 1);
        }
    }
}
