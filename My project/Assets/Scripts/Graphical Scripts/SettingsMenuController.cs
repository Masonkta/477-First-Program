using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject resumeArrow;
    public GameObject soundArrow;
    public GameObject[] soundBars; // Array of sound bars (GameObjects)

    private int selectedOption = 1;
    private int totalOptions = 2; // 1 for Sound, 2 for Resume
    private int soundLevel = 100; // Starts with 100% sound

    void Start()
    {
        UpdateArrowVisibility();
        UpdateSoundBars();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Handle up/down arrow navigation
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedOption = (selectedOption > 1) ? selectedOption - 1 : totalOptions;
            UpdateArrowVisibility();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedOption = (selectedOption < totalOptions) ? selectedOption + 1 : 1;
            UpdateArrowVisibility();
        }

        // Handle selection and changes based on selected option
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteSelection();
        }

        // Handle left/right navigation for sound control
        if (selectedOption == 1) // Sound option selected
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeSoundLevel(-5); // Decrease sound level
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeSoundLevel(5); // Increase sound level
            }
        }
    }

    void UpdateArrowVisibility()
    {
        // Hide arrows and show the correct one based on selection
        resumeArrow.SetActive(selectedOption == 2);
        soundArrow.SetActive(selectedOption == 1);
    }

    void ChangeSoundLevel(int change)
    {
        soundLevel = Mathf.Clamp(soundLevel + change, 0, 100); // Clamp between 0 and 100
        UpdateSoundBars(); // Update sound bars based on new level
    }

    void UpdateSoundBars()
    {
        // Calculate how many sound bars should be active
        int activeBars = Mathf.FloorToInt(soundLevel / 5f);

        // Update the sound bars by enabling/disabling them based on activeBars count
        for (int i = 0; i < soundBars.Length; i++)
        {
            soundBars[i].SetActive(i < activeBars); // Enable bars up to activeBars count
        }
    }


    void ExecuteSelection()
    {
        switch (selectedOption)
        {
            case 1: // Sound option selected
                // Optionally, add logic to save settings or make further adjustments
                break;
            case 2: // Resume game
                // Resume the game and close settings
                ResumeGame();
                break;
        }
    }

    void ResumeGame()
    {
        // Logic to resume the game (unpause, etc.)
        Time.timeScale = 1; // Unpause the game if it's paused

        // Unload the settings scene
        SceneManager.UnloadSceneAsync("Settings Scene");

        // Close the settings menu (if it's in the same object)
        gameObject.SetActive(false);
    }

}
