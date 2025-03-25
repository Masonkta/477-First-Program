using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOverController : MonoBehaviour
{
    public GameObject resumeArrow;

    void Start()
    {
        if (resumeArrow != null)
            resumeArrow.SetActive(true);
    }

    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        SceneManager.LoadScene("Start Screen"); // Reload the main scene
    }
}
