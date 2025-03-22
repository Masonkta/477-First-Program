using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject ResumeArrow;
    public GameObject SettingsArrow;
    public GameObject ExitArrow;
    public Movement playerMovement;

    private Game gameInstance;
    private int selectedOption = 1;
    private int totalOptions = 3;
    private bool isPaused = false;



    void Start()
    {
        gameInstance = FindObjectOfType<Game>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        pauseMenuUI.SetActive(false);
        EnableControls();
    }

    void Update()
    {
        if (IsSettingsSceneLoaded())
        {
            return; 
        }

        if ( IsSettingsSceneLoaded() == false && isPaused == false)
        {
            EnableControls();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }

        if (isPaused)
        {
            HandleInput();
        }
    }

    void EnableControls()
    {
        if (gameInstance != null && gameInstance.Input != null)
        {
            gameInstance.Input.Enable();
        }
    }

    void HandleInput()
    {
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
    }

    void UpdateArrowVisibility()
    {
        ResumeArrow.SetActive(selectedOption == 1);
        SettingsArrow.SetActive(selectedOption == 2);
        ExitArrow.SetActive(selectedOption == 3);
    }

    void ExecuteSelection()
    {
        switch (selectedOption)
        {
            case 1: 
                EnableControls();
                TogglePauseMenu();
                break;
            case 2: 
                DisableControls();
                OpenSettings();
                break;
            case 3: 
                DisableControls();
                SceneManager.LoadScene("Start Screen");
                break;
        }
    }

    void TogglePauseMenu()
    {
        
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        if (playerMovement != null)
        {
            playerMovement.enabled = !isPaused; 
        }
    }

    void OpenSettings()
    {
        SceneManager.LoadScene("Settings Scene", LoadSceneMode.Additive);
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
    }

    void DisableControls()
    {
        if (gameInstance != null && gameInstance.Input != null)
        {
            gameInstance.Input.Disable();
        }
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByName("Settings Scene").isLoaded)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    bool IsSettingsSceneLoaded()
    {
        return SceneManager.GetSceneByName("Settings Scene").isLoaded;
    }
}
