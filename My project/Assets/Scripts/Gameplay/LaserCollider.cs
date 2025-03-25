using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour {

    public AudioSource audioSource;
    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start() {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !collision.GetComponent<Movement>().isShielded) {
            Debug.Log("laser hit player!");

            FindObjectOfType<LifeManager>().LoseLife();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(explosion, 1f);
        }
        //else if (collision.CompareTag("Shield"))
        //{
        //    Destroy(this.gameObject);
        //    GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        //    audioSource.PlayOneShot(explosionSound);

        //    Destroy(explosion, 1f);
        //    Destroy(this.gameObject);
        //}
    }
}
