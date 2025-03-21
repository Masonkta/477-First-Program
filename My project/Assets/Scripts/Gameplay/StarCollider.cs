using UnityEngine;

public class StarController : MonoBehaviour
{

    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield")){
            Debug.Log("Star hit the Shield!");

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);
            Destroy(gameObject);
            Destroy(explosion, 0.5f);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Star hit the player!");

            FindObjectOfType<LifeManager>().LoseLife();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
