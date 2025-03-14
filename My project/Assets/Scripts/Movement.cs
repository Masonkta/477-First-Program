using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Game gameInstance;

    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    private Vector3 velocity = Vector3.zero;

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Start()
    {

        gameInstance = FindObjectOfType<Game>();

    }

    void Update()
    {
        if (gameInstance != null)
        {
            var input = gameInstance.Input.Controls;
            Vector3 inputDirection = Vector3.zero;

            if (input.Up.ReadValue<float>() != 0)
                inputDirection += Vector3.up;
            if (input.Down.ReadValue<float>() != 0)
                inputDirection += Vector3.down;
            if (input.Left.ReadValue<float>() != 0)
                inputDirection += Vector3.left;
            if (input.Right.ReadValue<float>() != 0)
                inputDirection += Vector3.right;

            if (input.Jump.ReadValue<float>() != 0 && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }

            inputDirection = inputDirection.normalized;


            if (inputDirection != Vector3.zero)
            {
                velocity = Vector3.Lerp(velocity, inputDirection * maxSpeed, Time.deltaTime * acceleration);
            }
            else
            {
                velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
            }

            transform.position += velocity * Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && spawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0, 0, 90));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(spawnPoint.forward * bulletSpeed, ForceMode.VelocityChange);
            }
        }
    }
}
