using UnityEngine;

public class EnemyCollider : MonoBehaviour
{

    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public AudioSource audioSource;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    public GameObject TopBoundary;
    public GameObject BottomBoundary;
    public float nextShotTime = 0.5f;
    public float shotDelay = 0.5f;
    public GameObject bulletPrefab; 
    public Transform spawnPoint; 
    public float bulletSpeed = 10f;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        Physics2D.IgnoreCollision(leftBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(rightBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(TopBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(BottomBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
    }

    void Update()
    {
        if (Time.time > nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + shotDelay;
            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");

            FindObjectOfType<LifeManager>().LoseLife();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(gameObject);
            Destroy(explosion, 1f);
        }

        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            Destroy(gameObject);
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

                rb.velocity = -spawnPoint.up * bulletSpeed;  
            }

            Destroy(bullet, 5f);
        }
    }
}