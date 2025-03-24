using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float moveSpeed;
    public AudioSource audioSource;
    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start() {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Border")) {
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(explosion, 1f);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("enemy bullet hit player!");

            FindObjectOfType<LifeManager>().LoseLife();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(explosion, 1f);
            Destroy(this.gameObject);
        }
    }
}
