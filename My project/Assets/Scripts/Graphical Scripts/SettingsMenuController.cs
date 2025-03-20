using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject resumeArrow;
    public GameObject soundArrow;
    public GameObject[] soundBars; 

    private int selectedOption = 1;
    private int totalOptions = 2; 
    private int soundLevel = 100; 

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


        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteSelection();
        }


        if (selectedOption == 1) 
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeSoundLevel(-5); 
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeSoundLevel(5);
            }
        }
    }

    void UpdateArrowVisibility()
    {
        resumeArrow.SetActive(selectedOption == 2);
        soundArrow.SetActive(selectedOption == 1);
    }

    void ChangeSoundLevel(int change)
    {
        soundLevel = Mathf.Clamp(soundLevel + change, 0, 100); 
        UpdateSoundBars(); 
    }

    void UpdateSoundBars()
    {
        int activeBars = Mathf.FloorToInt(soundLevel / 5f);
        for (int i = 0; i < soundBars.Length; i++)
        {
            soundBars[i].SetActive(i < activeBars); 
        }
    }


    void ExecuteSelection()
    {
        switch (selectedOption)
        {
            case 1: 
                break;
            case 2: 
                ResumeGame();
                break;
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1; 
        SceneManager.UnloadSceneAsync("Settings Scene");
        gameObject.SetActive(false);
    }

}
