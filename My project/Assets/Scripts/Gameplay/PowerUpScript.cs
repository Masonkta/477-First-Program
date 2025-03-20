using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    public string powerUpType;
    public GameObject ship;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerUpType == "Shield"){
                ship.GetComponent<Movement>().hasShield = true;
                Debug.Log("Player now has a shield.");
                Destroy(gameObject);
            }
            if (powerUpType == "FireRateUp"){
                ship.GetComponent<Movement>().hasFireRateUp = true;
                Destroy(gameObject);
            }
        }
    }
}
