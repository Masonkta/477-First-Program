using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Game gameInstance;
    private EnergyBar energyBarScript;
    private ShieldBar shieldBarScript;

    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    private Vector3 velocity = Vector3.zero;

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float bulletSpeed = 10f;
    public float fireRate = 3f;
    public float defaultFireRate = 3f;
    public float nextFireTime = 0f;

    public AudioClip soundEffect; 
    public AudioSource audioSource;
    public ParticleSystem thruster;

    public bool hasShield = false; 
    public float shieldDelay = 5f; 
    private float shieldShutOffTime;
    public bool isShielded = false;
    public GameObject shield;
    public bool hasFireRateUp = false;
    public bool hasFireRateDown = false;

    void Start()
    {
        gameInstance = FindObjectOfType<Game>();
        audioSource = Camera.main.GetComponent<AudioSource>();
        energyBarScript = FindObjectOfType<EnergyBar>();
        shieldBarScript = FindObjectOfType<ShieldBar>();
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
                energyBarScript.FireLaser(fireRate);
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

            GameObject shieldObject = transform.Find("Shield Particle System")?.gameObject;

            if (hasShield && shieldObject != null)
            {
                shieldObject.SetActive(true);
                shieldShutOffTime = Time.time + shieldDelay;
                hasShield = false;
                isShielded = true;
                shieldBarScript.ActivateShield(shieldDelay);
                GetComponent<Collider2D>().enabled = false;
            }

            if (shieldObject != null)
            {
                if (shieldShutOffTime < Time.time)
                {
                    shieldObject.SetActive(false);
                    isShielded = false;
                    GetComponent<Collider2D>().enabled = true;
                }
            }

            if (hasFireRateUp){
                fireRate = 0f;
                hasFireRateUp = false;
            }

            if (fireRate < defaultFireRate){
                fireRate += 0.0001f;
            }

            if (hasFireRateDown){
                fireRate = 4f;
                hasFireRateDown = false;
            }

            if (fireRate > defaultFireRate){
                fireRate -= 0.001f;
            }
            AdjustThrusterEffect(inputDirection);

            transform.position += velocity * Time.deltaTime;
        }
    }

    void AdjustThrusterEffect(Vector3 inputDirection)
    {
        if (thruster != null)
        {

            if (inputDirection.x < 0)
            {
                thruster.Stop();
            }
            else
            {
 
                if (!thruster.isPlaying)
                {
                    thruster.Play();
                }
            }
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
}
