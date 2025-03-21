using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject ImpactPrefab;
    public AudioClip bulletHitSound;
    public AudioClip explosionSound;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield") || other.CompareTag("Border") || other.CompareTag("PowerUp") || other.CompareTag("Player") || other.CompareTag("Bullet")) 
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(explosionSound);

            GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject); 
            Destroy(gameObject); 
            Destroy(explosion, 1f);
            Destroy(impact, 1f);
        }
        else
        {
            audioSource.PlayOneShot(bulletHitSound);

            GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
            Destroy(impact, 1f);
        }
    }

    // called in boss script
    public void explosion() {
        audioSource.PlayOneShot(explosionSound);

        GameObject impact = Instantiate(ImpactPrefab, transform.position, Quaternion.identity);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(explosion, 1f);
        Destroy(impact, 1f);
        Destroy(gameObject);
    }
}
