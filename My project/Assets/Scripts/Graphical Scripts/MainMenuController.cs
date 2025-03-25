using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    public GameObject onePlayerArrow;
    public GameObject settingsArrow;

    private int selectedOption = 1;
    private int totalOptions = 2;

    void Start()
    {
        Time.timeScale = 1f;
        int savedVolume = PlayerPrefs.GetInt("VolumeLevel", 100);
        AudioListener.volume = savedVolume / 100f;
        UpdateArrowVisibility();
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            selectedOption = (selectedOption > 1) ? selectedOption - 1 : totalOptions;
            UpdateArrowVisibility();
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
        {
            selectedOption = (selectedOption < totalOptions) ? selectedOption + 1 : 1;
            UpdateArrowVisibility();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ExecuteSelection();
        }
    }

    void UpdateArrowVisibility()
    {
        if (onePlayerArrow != null)
            onePlayerArrow.SetActive(selectedOption == 1);

        if (settingsArrow != null)
            settingsArrow.SetActive(selectedOption == 2);
    }

    void ExecuteSelection()
    {
        switch (selectedOption)
        {
            case 1: // One Player Mode
                PlayerPrefs.SetInt("PlayerMode", 1);
                SceneManager.LoadScene("Main Scene");
                break;

            case 2: // Open Settings
                SceneManager.LoadScene("Title Settings");
                break;
        }
    }
}