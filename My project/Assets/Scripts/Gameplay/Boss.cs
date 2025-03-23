using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour{
    #region publics
    public float health;
    public float bulletSpeed;
    public Transform playerPos;
    public float moveSpeed;
    public float laserFireRate = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnTop;
    public Transform bulletSpawnBottom;
    public float bulletFireRate = .5f;
    public GameObject misslePrefab;
    public Transform missleSpawnTop;
    public Transform missleSpawnBottom;
    public float missleSpawnFreq = 10f;
    public float moveInterval;
    public Slider healthBar;
    #endregion

    #region privates
    private float maxHealth = 100f;
    private float nextBulletFire;
    private float nextlaser;
    private float nextmissle;
    private float moveTimer = 100;
    private Vector3 playerY;
    private Transform bossMove;
    private bool inScreen = false;
    #endregion

    // Start is called before the first frame update
    void Start(){
        health = maxHealth;
        bossMove = transform;
        bossMove.Translate(new Vector3(8.5f, 0, 0));
        misslePrefab.GetComponent<Missle>().playerPos = playerPos;
    }

    // Update is called once per frame
    void Update(){
        //healthBar.value = health/maxHealth;
        print(playerPos.position);

        if (bossMove.position.x > 3.5) {
            bossMove.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            health = maxHealth;
        } else {
            inScreen = true;
        }

        if (inScreen) {
            // Attack timings, prioities: spawning, laser, bullets
            if (nextmissle > missleSpawnFreq) {
                spawnMissles();
                nextmissle = 0;
                nextBulletFire -= 5f;
                nextlaser -= 5f;
                moveTimer -= 5f;
            }
            else if (nextlaser > laserFireRate) {
                FindObjectOfType<LaserBeamFade>().FireLaser();
                nextmissle -= 4.5f;
                nextlaser = 0;
                nextBulletFire = -4.5f;
                moveTimer -= 4.5f;
            }
            else if (nextBulletFire > bulletFireRate) {
                shoot();
                nextBulletFire = 0;
            }

            // move based on player space, move every 5 seconds
            if (moveTimer > moveInterval) {
                playerY = new Vector3(bossMove.transform.position.x, playerPos.position.y, 0);
                if (playerPos.position.y > 2.5) {
                    playerY = new Vector3(bossMove.transform.position.x, 2.5f, 0);
                } else if (playerPos.position.y < -2) {
                    playerY = new Vector3(bossMove.transform.position.x, -2, 0);
                }
                moveTimer = 0;
            }

            moveToPlayer();
            nextmissle += Time.deltaTime;
            nextlaser += Time.deltaTime;
            nextBulletFire += Time.deltaTime;
            moveTimer += Time.deltaTime;
        }
    }   


    void shoot() {
        if (Math.Abs(playerPos.position.y - bulletSpawnTop.position.y) < Math.Abs(playerPos.position.y - bulletSpawnBottom.position.y)) { 
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTop);
        } else {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnBottom);
        }
    }

    void spawnMissles() {
        for (int i = 0; i < 4; i++) {
            if (i % 2 == 0) {
                GameObject missle = Instantiate(misslePrefab, missleSpawnTop);
            } else {
                GameObject missle = Instantiate(misslePrefab, missleSpawnTop);
            }
            
        }
    }

    void moveToPlayer() {
        bossMove.position = Vector3.MoveTowards(bossMove.position, playerY, moveSpeed * Time.deltaTime);
    }

    public void decHealth(float amount) {
        health -= amount;
        if (health < 0) {
            health = 0;
            nextmissle -= 40000f;
            nextlaser -= 400000;
            nextBulletFire = -450000f;
            moveTimer -= 450000f;
            // death animation
        }
    }
}
