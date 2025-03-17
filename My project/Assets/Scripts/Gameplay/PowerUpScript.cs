using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    public string powerUpType;
    public GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerUpType == "Shield"){
                ship.GetComponent<Movement>().hasShield = true;
            }
        }
    }
}
