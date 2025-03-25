using UnityEngine;

public class DebrisController : MonoBehaviour
{

    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public AudioSource audioSource;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    public GameObject TopBoundary;
    public GameObject BottomBoundary;

    void Start() {
        audioSource = Camera.main.GetComponent<AudioSource>();
        Physics2D.IgnoreCollision(leftBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(rightBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(TopBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(BottomBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Debris hit the player!");

            FindObjectOfType<LifeManager>().LoseLife();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(gameObject);
            Destroy(explosion, 1f);
            
        } else if (other.CompareTag("Shield")) {
            Destroy(this.gameObject);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(explosion, 1f);
            Destroy(this.gameObject);

        }

        if (other.CompareTag("Bullet")) {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("PowerUp")) {
            Destroy(gameObject);
        }
    }
}