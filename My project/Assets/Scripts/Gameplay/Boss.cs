using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour{
    public float health;
    private float maxHealth = 100f;
    public float bulletSpeed;
    public Transform playerPos;
    public float nextBulletFire;
    public float nextlaser;
    public float nextEnemies;
    public float moveSpeed;

    public GameObject laserPrefab;
    public Transform laserSpawn;
    public float laserFireRate = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletFireRate = 1f;
    public GameObject enemyPrefab;
    public Transform enemySpawn;
    public float enemySpawnFreq = 10f;



    // Start is called before the first frame update
    void Start(){
        health = maxHealth;
    }

    // Update is called once per frame
    void Update(){
        var bossMove = transform;

        // Attack timings, prioities: spawning, laser, bullets
        if (Time.time >= nextEnemies) {
            spawnDrones();
            nextEnemies += nextEnemies;
        } else if (Time.time >= nextlaser) {
            laser();
            nextlaser += laserFireRate;
        } else if (Time.time >= nextBulletFire) {
            shoot();
            nextBulletFire += bulletFireRate;
        }

        // move based on player space, move every 5 seconds
        if (Time.time % 5 == 0) {
            Vector3 playerY = new Vector3(0, playerPos.position.y, 0);
            bossMove.Translate(playerY * moveSpeed * Time.deltaTime);
        }
    }   


    void shoot() {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null) {
            rb.velocity = bulletSpawn.up * bulletSpeed;
        }

        Destroy(bullet, 5f);
    }


    void laser() {
        GameObject las = Instantiate(laserPrefab, laserSpawn);
        Rigidbody2D rb = las.GetComponent<Rigidbody2D>();

        if (rb != null) {
            rb.velocity = bulletSpawn.up * bulletSpeed;
        }

        Destroy(las, 15f);
    }

    void spawnDrones() {

    }
}
