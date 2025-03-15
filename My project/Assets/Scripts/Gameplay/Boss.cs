using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour{
    public float health;
    private float maxHealth = 100f;
    public float bulletSpeed;
    public Transform playerPos;
    public float nextBulletFire;
    public float nextLazer;
    public float moveSpeed;

    public GameObject lazerPrefab;
    public Transform lazerSpawn;
    public float lazerFireRate = 5f;
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
        var bossMove = this.transform;
        //if (playerPos.position.y < bulletSpawn.position.y + 3 || playerPos.position.y > bulletSpawn.position.y - 3){
        //    shoot();
        //}

        // make mutually exlucisve 
        if (Time.time >= nextBulletFire) {
            shoot();
            nextBulletFire = Time.time + bulletFireRate;
        }
        if (Time.time >= nextLazer) {
            lazer();
            nextLazer = Time.time + lazerFireRate;
        }

        if (Time.time % 5 == 0) {
            //bossMove.position.y = playerPos.position.y;
            // move based on player space, move every 5 seconds
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


    void lazer() {
        GameObject laz = Instantiate(lazerPrefab, lazerSpawn);
        Rigidbody2D rb = laz.GetComponent<Rigidbody2D>();

        if (rb != null) {
            rb.velocity = bulletSpawn.up * bulletSpeed;
        }

        Destroy(laz, 15f);
    }
}
