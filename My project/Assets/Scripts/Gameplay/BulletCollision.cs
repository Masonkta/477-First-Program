using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    public GameObject explosionPrefab;
    public AudioClip bulletHitSound;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit the enemy!");

            audioSource.PlayOneShot(bulletHitSound);

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);


            Destroy(other.gameObject); 
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
