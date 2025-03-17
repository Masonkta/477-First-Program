using UnityEngine;

public class DebrisController : MonoBehaviour
{

    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public AudioSource audioSource;
    public GameObject leftBoundary;
    public GameObject rightBoundary;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        Physics2D.IgnoreCollision(leftBoundary.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(rightBoundary.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Debris hit the player!");


            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }

        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
        }

    }
}