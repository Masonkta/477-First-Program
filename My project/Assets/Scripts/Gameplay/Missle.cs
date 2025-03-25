using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missle : MonoBehaviour {

    public Transform playerPos;
    public float moveSpeed;
    public AudioSource audioSource;
    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    private Vector3 target;


    // Start is called before the first frame update
    void Start() {
        target = playerPos.position;
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        //transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        //target.position * moveSpeed * Time.deltaTime
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.velocity = target * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Border")) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("enemy bullet hit player!");
            FindObjectOfType<LifeManager>().LoseLife();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);
            Destroy(explosion, 1f);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);

            Destroy(explosion, 1f);
            Destroy(this.gameObject);
        }
    }
}
