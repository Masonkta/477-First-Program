using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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

    void Start()
    {
        gameInstance = FindObjectOfType<Game>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        debrisPrefabs = new List<GameObject> {debris1Prefab, debris2Prefab, debris3Prefab};
        powerUpPrefabs = new List<GameObject> {shieldPowerUpPrefab, fireRateUpPowerUpPrefab, fireRateDownPowerUpPrefab};
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

            if (Time.time > nextDebrisTime)
            {
                FloatingDebris();
                nextDebrisTime = Time.time + debrisDelay;
            }

            if (Time.time > nextPowerUpTime)
            {
                FloatingPowerUps();
                nextPowerUpTime = Time.time + PowerUpDelay;
            }

            if (Time.time > nextEnemyFleetTime){
                StartCoroutine(EnemyFleet());
                nextEnemyFleetTime = Time.time + enemyFleetDelay;
            }
        }
    }

    void ShootingStar(){
        ShootingStarSpawnpoint.position = new Vector2(Random.Range(-1f, 10f), ShootingStarSpawnpoint.transform.position.y);
        GameObject star = Instantiate(starPrefab, ShootingStarSpawnpoint.position, Quaternion.identity);

        Rigidbody2D rb = star.GetComponent<Rigidbody2D>();

        if (rb != null)
            {

                rb.velocity = ShootingStarSpawnpoint.up * starSpeed;  
            }

            Destroy(star, 6f);
    }

    void FloatingDebris(){
        // Set a random Y position while keeping the X fixed.
        debrisSpawnpoint.position = new Vector3(debrisSpawnpoint.transform.position.x, Random.Range(-3f, 6f), -0.2f);

        GameObject debrisPrefab = debrisPrefabs[Random.Range(0, debrisPrefabs.Count)];

        // Instantiate the debris.
        GameObject debris = Instantiate(debrisPrefab, debrisSpawnpoint.position, Quaternion.identity);

        // Get Rigidbody2D component.
        Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

        // Set velocity to move from right to left.
        rb.velocity = Vector2.left * debrisSpeed;

        // Destroy the debris after 15 seconds.
        Destroy(debris, 25f);
    }

    void FloatingPowerUps(){
        // Set a random Y position while keeping the X fixed.
        powerUpSpawnpoint.position = new Vector3(powerUpSpawnpoint.transform.position.x, Random.Range(-3f, 6f), -0.2f);

        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];

        // Instantiate the debris.
        GameObject powerUp = Instantiate(powerUpPrefab, powerUpSpawnpoint.position, Quaternion.identity);

        powerUp.GetComponent<PowerUpScript>().ship = GameObject.Find("Ship");

        // Get Rigidbody2D component.
        Rigidbody2D rb = powerUp.GetComponent<Rigidbody2D>();

        // Set velocity to move from right to left.
        rb.velocity = Vector2.left * powerUpSpeed;

        // Destroy the debris after 15 seconds.
        Destroy(powerUp, 25f);
    }

    IEnumerator EnemyFleet(){
        // Instantiate the enemies.
        List<GameObject> enemies = new List<GameObject>(new GameObject[5]);
        for (int i = 0; i < 5; i++){
            // Set a random Y position while keeping the X fixed.
            enemySpawnpoint.position = new Vector3(enemySpawnpoint.transform.position.x, Random.Range(-3f, 6f), -0.2f);

            //spawn enemy
            enemies[i] = Instantiate(enemyPrefab, enemySpawnpoint.position, Quaternion.identity);

            // Get Rigidbody2D component.
            Rigidbody2D rb = enemies[i].GetComponent<Rigidbody2D>();

            // Set velocity to move from right to left.
            rb.velocity = Vector2.left * enemySpeed;

            // Destroy the enemy after 15 seconds.
            Destroy(enemies[i], 10f);

            yield return new WaitForSeconds(0.5f);
        }
    }

}
