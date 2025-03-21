using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiver : MonoBehaviour
{
    public float maxSpeed = .5f;
    public float randSeekTimerMin = 10f;
    public float randSeekTimerMax = 15f;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    public GameObject TopBoundary;
    public GameObject BottomBoundary;



    public float attackSpeed = 1.5f;

        // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(leftBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(rightBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(TopBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        Physics2D.IgnoreCollision(BottomBoundary.GetComponent<BoxCollider2D>(), GetComponent<PolygonCollider2D>());
        this.transform.Translate(Vector2.left * Time.deltaTime);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        // every y seconds move in a random direction up or down
        // at some point randomly gravitate toward the player
    }

}