using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDiver : MonoBehaviour
{
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    public GameObject TopBoundary;
    public GameObject BottomBoundary;

    public float maxSpeed = .7f;
    public float randSeekTimerMin = 8f;
    public float randSeekTimerMax = 12f;

    public float moveFrequencyMax = 1.5f;
    public float moveFrequencyMin = 1f;
    public float moveForce = 200;

    public float attackTime = 4f;
    public float attackTimeMin = 1f;
    public float attackTimeMax = 2f;

    public float upperSpawn = 4;
    public float lowerSpawn = -4;

    public AudioSource audioSource;
    public AudioClip explosionSound;

    public GameObject bullet;
    public GameObject explosion;
    public Transform bulletSpawn;
    private GameObject playerLoc;

    private float timer = 0;
    private float randSeekTime;
    private float moveFrequency;
    private float diveBombTime = 0;
    private Rigidbody2D rb;
    private bool hasDived = false;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
        Physics2D.IgnoreCollision(leftBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(rightBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(TopBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(BottomBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        this.transform.Translate(Vector2.left * Time.deltaTime);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * maxSpeed;

        transform.position = new Vector3(rb.position.x, Random.Range(lowerSpawn, upperSpawn), .3f);
        randSeekTime = Random.Range(randSeekTimerMin, randSeekTimerMax);
        moveFrequency = Random.Range(moveFrequencyMin, moveFrequencyMax);
        playerLoc = GameObject.Find("Ship");
        audioSource = Camera.main.GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name.Equals("Level 2"))
        {
            maxSpeed = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            attackTime += Random.Range(attackTimeMin, attackTimeMax);
            Instantiate(bullet, bulletSpawn);
            
        }
        if (timer > randSeekTime)
        {
            if (!hasDived)
            {
                hasDived = true;
                direction = (playerLoc.transform.position - transform.position).normalized;
            }
            rb.velocity = -direction * maxSpeed * (1+diveBombTime-randSeekTime);

            
            if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10)
            {
                Destroy(this.gameObject);
            }
        }
        if (timer >= moveFrequency)
        {
            moveFrequency += Random.Range(moveFrequencyMin, moveFrequencyMax);
            if (this.transform.position.y  >= 5f)
            {
                rb.velocity += Vector2.down * maxSpeed * .5f;
            }
            else if (this.transform.position.y <= -5f)
            {
                rb.velocity += Vector2.up * maxSpeed * .5f;
            }
            else
            {
                if(Random.Range(0f, 1f) > .5f)
                {
                    rb.velocity += Vector2.down * maxSpeed * .5f;

                }
                else
                {
                    rb.velocity += Vector2.up * maxSpeed * .5f;
                }
            }
        }       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            GameObject explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);
            Destroy(explosionEffect, 1f);
            Destroy(this.gameObject);
            FindObjectOfType<ScoreManager>().updateScore(200);
        }
        else if (other.CompareTag("Player"))
        {
            GameObject explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(explosionSound);
            Destroy(explosionEffect, 1f);
            Destroy(this.gameObject);
            FindObjectOfType<LifeManager>().LoseLife();
        }
    }
    
}