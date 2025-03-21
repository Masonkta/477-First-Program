using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int lives = 3; // Player starts with 3 lives
    public GameObject[] lifeIcons; // UI Life icons
    public Transform respawnPoint; // Assign a respawn point in Unity
    public GameObject playerPrefab; // Player prefab
    private GameObject playerInstance;

    private void Start()
    {
        playerInstance = GameObject.FindGameObjectWithTag("Player"); // Find the player at start
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;

            // Hide the corresponding life icon
            if (lives < lifeIcons.Length)
            {
                lifeIcons[lives].SetActive(false);
            }

            if (lives > 0)
            {
                RespawnPlayer();
            }
            else
            {
                GameOver();
                Destroy(playerInstance);
            }
        }
    }

    void RespawnPlayer()
    {
        Debug.Log("Respawning player...");

        // Destroy old player instance if it exists
        if (playerInstance != null)
        {
            playerInstance.transform.position = respawnPoint.position;
            playerInstance.SetActive(true);
            playerInstance.GetComponent<Movement>().hasShield = true;

        }

        // Instantiate a new player at the respawn point

    }

    void GameOver()
    {
        Debug.Log("Game Over! Player has lost all lives.");
        SceneManager.LoadScene("GameOver Screen");
    }
}
