using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour{
    #region publics
    public float health;
    private float maxHealth = 100f;
    public float bulletSpeed;
    public Transform playerPos;
    public float moveSpeed;
    public float laserFireRate = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnTop;
    public Transform bulletSpawnBottom;
    public float bulletFireRate = 1f;
    public GameObject enemyPrefab;
    public Transform enemySpawnTop;
    public Transform enemySpawnBottom;
    public float enemySpawnFreq = 10f;
    public float moveInterval;
    #endregion

    #region privates
    private float nextBulletFire;
    private float nextlaser;
    private float nextEnemies;
    private float moveTimer = 100;
    private bool lasering;
    private Vector3 playerY;
    private Transform bossMove;
    #endregion

    // Start is called before the first frame update
    void Start(){
        health = maxHealth;
        bossMove = transform;
        bossMove.Translate(new Vector3(0, 0, 0));
        //lasering = FindObjectOfType<LaserBeamFade>()
    }

    // Update is called once per frame
    void Update(){
        // Attack timings, prioities: spawning, laser, bullets
        if (nextEnemies > enemySpawnFreq) {
            print("enemy spawn");
            spawnDrones();
            nextEnemies = 0;
            
        } else if (nextlaser > laserFireRate) {
            FindObjectOfType<LaserBeamFade>().FireLaser();
            // pause all movement and other actions
            nextlaser = 0;
        } else if (nextBulletFire > bulletFireRate) {
            shoot();
            nextBulletFire = 0;
        }

        // move based on player space, move every 5 seconds
        if (moveTimer > moveInterval) {
            //playerY = new Vector3(bossMove.transform.position.x, playerPos.position.y, 0);
            playerY = new Vector3(bossMove.position.x, UnityEngine.Random.Range(-2f, 2f), 0);

            moveTimer = 0;
        }

        moveToPlayer();
        nextEnemies += Time.deltaTime;
        nextlaser += Time.deltaTime;
        nextBulletFire += Time.deltaTime;
        moveTimer += Time.deltaTime;
    }   


    void shoot() {
        if (Math.Abs(playerPos.position.y - bulletSpawnTop.position.y) > Math.Abs(playerPos.position.y - bulletSpawnBottom.position.y)) { 
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTop);
        } else {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnBottom);
        }
        
    }

    void spawnDrones() {

    }

    void moveToPlayer() {
        bossMove.position = Vector3.MoveTowards(bossMove.position, playerY, moveSpeed * Time.deltaTime);
    }
}
