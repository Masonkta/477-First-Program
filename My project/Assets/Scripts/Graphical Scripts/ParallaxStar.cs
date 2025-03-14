using UnityEngine;

public class ParallaxStar : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Destroy the star when it moves off-screen
        if (transform.position.x < -10f) // Adjust this value based on your screen size
        {
            Destroy(gameObject);
        }
    }
}
