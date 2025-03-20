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
    public float minSpeed; 
    public float maxSpeed; 

    void OnEnable()
    {
        StartCoroutine(SpawnStar());
    }

    IEnumerator SpawnStar()
    {
        while (true)
        {
            int starCount = Random.Range(3, 7); 

            for (int i = 0; i < starCount; i++)
            {
                Vector2 spawnPos = new Vector2(
                    Random.Range(minSpawnPos.x, maxSpawnPos.x),
                    Random.Range(minSpawnPos.y, maxSpawnPos.y)
                );

                GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);
                float randomScale = Random.Range(minScale, maxScale);
                star.transform.localScale = new Vector3(randomScale, randomScale, 1);

                // Add parallax effect
                ParallaxStar parallax = star.AddComponent<ParallaxStar>();
                parallax.speed = Mathf.Lerp(maxSpeed, minSpeed, (randomScale - minScale) / (maxScale - minScale));
            }

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}
