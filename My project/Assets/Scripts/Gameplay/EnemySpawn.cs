using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
    public float spawnTimeMin = 3f;
    public float spawnTimeMax = 5f;
    public float numSpawnedMax = 4;
    public float numSpawnedMin = 1;
    

    public GameObject enemy;
    public Transform spawnPoint;
    private Transform spawnTarget;

    private float timer = 0f;
    private float randomFloat = 2;
    private float numSpawned = 1;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnTarget = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= randomFloat)
        {

            timer = 0;
            randomFloat = Random.Range(spawnTimeMin, spawnTimeMax);
            for (int i = 0; i < numSpawned; i++)
            {
               GameObject diver = Instantiate(enemy, spawnTarget);
                spawnedObjects.Add(diver);
            }
        }
    }
    public void DestroyAllSpawnedObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear(); // Clear the list after destroying objects
    }
}
