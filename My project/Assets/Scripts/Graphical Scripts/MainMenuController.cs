using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // If using the new Input System

public class MainMenuController : MonoBehaviour
{
    public GameObject onePlayerArrow;
    public GameObject twoPlayerArrow;

    private int selectedOption = 1;

    void Start()
    {
        Time.timeScale = 1f;
        UpdateArrowVisibility();
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            selectedOption = 1;
            UpdateArrowVisibility();
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
        {
            selectedOption = 2;
            UpdateArrowVisibility();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            LoadMainScene();
        }
    }

    void UpdateArrowVisibility()
    {
        if (onePlayerArrow != null)
            onePlayerArrow.SetActive(selectedOption == 1);

        if (twoPlayerArrow != null)
            twoPlayerArrow.SetActive(selectedOption == 2);
    }

    void LoadMainScene()
    {
        PlayerPrefs.SetInt("PlayerMode", selectedOption);

        SceneManager.LoadScene("Main Scene");
    }

}
