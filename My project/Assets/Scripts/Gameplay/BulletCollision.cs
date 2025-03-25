using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject ImpactPrefab;
    public AudioClip bulletHitSound;
    public AudioClip explosionSound;
    public AudioSource audioSource;
    public Boss boss;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        boss = FindAnyObjectByType<Boss>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield") || other.CompareTag("Border") || other.CompareTag("PowerUp") || other.CompareTag("Player") || other.CompareTag("Bullet")) 
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            audioSource.PlayOneShot(explosionSound);
            GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject); 
            Destroy(gameObject); 
            Destroy(explosion, 1f);
            Destroy(impact, 1f);
            FindObjectOfType<ScoreManager>().updateScore(200);
        }
        else if (other.CompareTag("Boss"))
        {
            audioSource.PlayOneShot(bulletHitSound);
            Debug.Log("BOSS HIT");

            GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(impact, 1f);
            boss.decHealth(1);
            FindObjectOfType<ScoreManager>().updateScore(100);

        }
        else
        {
            audioSource.PlayOneShot(bulletHitSound);

            GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
            Destroy(impact, 1f);
        }
    }
}
