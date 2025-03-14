using System.Collections;
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

    public AudioClip soundEffect; 
    public AudioSource audioSource;
    public float starDelay;
    private float nextStarTime = 10f;
    public Transform ShootingStarSpawnpoint;
    public GameObject starPrefab;
    public float starSpeed; 

    void Start()
    {
        gameInstance = FindObjectOfType<Game>();
        audioSource = FindObjectOfType<AudioSource>();
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
                audioSource.PlayOneShot(soundEffect);
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

            if (Time.time > nextStarTime)
            {
                ShootingStar();
                nextStarTime = Time.time + starDelay;
            }

            transform.position += velocity * Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && spawnPoint != null)
        {

            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler(0,0,90));


            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {

                rb.velocity = spawnPoint.up * bulletSpeed;  
            }

            Destroy(bullet, 5f);
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
}
