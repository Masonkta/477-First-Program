using UnityEngine;

public class StarCollision : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Star hit the player!");


            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
