using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float spawnTimeMin = 3f;
    public float spawnTimeMax = 5f;
    public GameObject enemy;
    public Transform spawnPoint;

    private float timer = 0f;
    private float randomFloat = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= randomFloat)
        {
            timer = 0;
            randomFloat = Random.Range(spawnTimeMin, spawnTimeMax);
            Instantiate(enemy, spawnPoint);
        }
    }
}
