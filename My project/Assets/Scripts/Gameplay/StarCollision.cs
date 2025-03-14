using UnityEngine;

public class StarCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Star hit the player!"); 
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
