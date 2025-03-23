using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiver : MonoBehaviour
{
    public float maxSpeed = .5f;
    public float randSeekTimerMin = 8f;
    public float randSeekTimerMax = 12f;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    public GameObject TopBoundary;
    public GameObject BottomBoundary; 
    public float attackSpeed = 1.5f;
    public float upperSpawn = 4;
    public float lowerSpawn = -4;

    public GameObject bullet;
    public GameObject explosion;
    public Transform bulletSpawn;
    private GameObject playerLoc;

    private float timer = 0;
    private float attackTime;
    private float randSeekTime;
    private float diveBombTime = 0;
    private Rigidbody2D rb;
    private bool hasDived = false;
    private Vector2 direction;
    private bool isDestroyed = false;
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

        attackTime = 7;
        randSeekTime = Random.Range(randSeekTimerMin, randSeekTimerMax);
        playerLoc = GameObject.Find("Ship");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        diveBombTime += Time.deltaTime;
        if (timer >= attackTime)
        {
            attackTime = Random.Range(attackSpeed - .7f, attackSpeed + .7f);
            Instantiate(bullet, bulletSpawn);
            timer = 0;
        }
        if (diveBombTime > randSeekTime)
        {
            if (!hasDived)
            {
                hasDived = true;
                direction = (playerLoc.transform.position - transform.position).normalized;
            }
            rb.velocity = direction * maxSpeed * (1+diveBombTime-randSeekTime);

            
            if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 10)
            {
                Destroy(this.gameObject);
            }
        }
        if (isDestroyed)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Instantiate(explosion, bulletSpawn);
            isDestroyed = true;
        }
        if (other.CompareTag("Bullet"))
        {
            Instantiate(explosion, bulletSpawn);
            Destroy(other.gameObject);
            isDestroyed = true;
            
        }
    }
}