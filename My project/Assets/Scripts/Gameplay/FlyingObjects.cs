using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingObjects : MonoBehaviour
{
    private Game gameInstance;
    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public AudioSource audioSource;

    public float starDelay;
    public float nextStarTime = 10f;
    public Transform ShootingStarSpawnpoint;
    public GameObject starPrefab;
    public float starSpeed;

    public float debrisDelay;
    public float nextDebrisTime = 5f;
    public Transform debrisSpawnpoint;
    public GameObject debris1Prefab;
    public GameObject debris2Prefab;
    public GameObject debris3Prefab;
    public List<GameObject> debrisPrefabs;
    public float debrisSpeed;
    public float nextPowerUpTime = 12f;
    public float PowerUpDelay = 12f;
    public Transform powerUpSpawnpoint;
    public GameObject shieldPowerUpPrefab;
    public GameObject fireRateUpPowerUpPrefab;
    public GameObject fireRateDownPowerUpPrefab;
    public List<GameObject> powerUpPrefabs;
    public float powerUpSpeed;
    public float nextEnemyFleetTime = 20f;
    public float enemyFleetDelay = 20f;
    public Transform enemySpawnpoint;
    public GameObject enemyPrefab;
    public float enemySpeed = 7f;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // List to track spawned objects

    void Start()
    {
        gameInstance = FindObjectOfType<Game>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        debrisPrefabs = new List<GameObject> { debris1Prefab, debris2Prefab, debris3Prefab };
        powerUpPrefabs = new List<GameObject> { shieldPowerUpPrefab, fireRateUpPowerUpPrefab, fireRateDownPowerUpPrefab };
        powerUpSpeed = debrisSpeed;
    }

    void Update()
    {
        if (gameInstance != null)
        {
            if (Time.time > nextStarTime)
            {
                ShootingStar();
                nextStarTime = Time.time + starDelay;
            }

            if (Time.time > nextDebrisTime && SceneManager.GetActiveScene().name != "Level 3")
            {
                FloatingDebris();
                nextDebrisTime = Time.time + debrisDelay;
            }

            if (Time.time > nextPowerUpTime)
            {
                FloatingPowerUps();
                nextPowerUpTime = Time.time + PowerUpDelay;
            }

            if (Time.time > nextEnemyFleetTime && SceneManager.GetActiveScene().name != "Level 3")
            {
                StartCoroutine(EnemyFleet());
                nextEnemyFleetTime = Time.time + enemyFleetDelay;
            }
        }
    }

    void ShootingStar()
    {
        ShootingStarSpawnpoint.position = new Vector2(Random.Range(-1f, 10f), ShootingStarSpawnpoint.transform.position.y);
        GameObject star = Instantiate(starPrefab, ShootingStarSpawnpoint.position, Quaternion.identity);
        spawnedObjects.Add(star); // Track object

        Rigidbody2D rb = star.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = ShootingStarSpawnpoint.up * starSpeed;
        }

        Destroy(star, 6f);
    }

    void FloatingDebris()
    {
        debrisSpawnpoint.position = new Vector3(debrisSpawnpoint.transform.position.x, Random.Range(-3f, 6f), -0.2f);
        GameObject debrisPrefab = debrisPrefabs[Random.Range(0, debrisPrefabs.Count)];
        GameObject debris = Instantiate(debrisPrefab, debrisSpawnpoint.position, Quaternion.identity);
        spawnedObjects.Add(debris); // Track object

        Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * debrisSpeed;

        Destroy(debris, 25f);
    }

    void FloatingPowerUps()
    {
        powerUpSpawnpoint.position = new Vector3(powerUpSpawnpoint.transform.position.x, Random.Range(-3f, 6f), -0.2f);
        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];
        GameObject powerUp = Instantiate(powerUpPrefab, powerUpSpawnpoint.position, Quaternion.identity);
        spawnedObjects.Add(powerUp); // Track object

        powerUp.GetComponent<PowerUpScript>().ship = GameObject.Find("Ship");

        Rigidbody2D rb = powerUp.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * powerUpSpeed;

        Destroy(powerUp, 25f);
    }

    IEnumerator EnemyFleet()
    {
        List<GameObject> enemies = new List<GameObject>();
        int fleetSize = 1;
        if(SceneManager.GetActiveScene().name.Equals("Level 2")){
            fleetSize = 5;
        }
        for (int i = 0; i < fleetSize; i++)
        {
            enemySpawnpoint.position = new Vector3(enemySpawnpoint.transform.position.x, Random.Range(-3f, 6f), -0.2f);
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnpoint.position, Quaternion.identity);
            spawnedObjects.Add(enemy); // Track object
            enemies.Add(enemy);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.left * enemySpeed;

            Destroy(enemy, 10f);

            yield return new WaitForSeconds(0.5f);
        }
    }

    // Function to destroy all instantiated objects
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
        Debug.Log("All instantiated objects have been destroyed.");
    }
}
