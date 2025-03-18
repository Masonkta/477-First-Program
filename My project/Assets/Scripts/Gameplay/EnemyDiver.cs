using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiver : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float diveAccel = 1f;

    public GameObject bulletPrefab;
    public Transform spawnPoint;

    public float randShootMin = .5f;
    public float randShootMax = 3f;

    public float restTime = 2f;
    public float randMoveTimeMin = 1f;
    public float randMoveTimeMax = 3f;

    public float randDiveMin = 10f;
    public float randDiveMax = 20f;

    public Transform playerLocation;


    // Start is called before the first frame update
    void Start()
    {
        print(Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        // every y seconds move in a random direction up or down
        // at some point randomly gravitate toward the player
    }

}