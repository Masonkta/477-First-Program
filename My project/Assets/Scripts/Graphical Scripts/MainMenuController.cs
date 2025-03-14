using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject onePlayerArrow;   
    public GameObject twoPlayerArrow;   

    private int selectedOption = 1;

    void Start()
    {
        UpdateArrowVisibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedOption = 1;
            UpdateArrowVisibility();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedOption = 2;
            UpdateArrowVisibility();
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainScene();
        }
    }

    void UpdateArrowVisibility()
    {
        onePlayerArrow.SetActive(selectedOption == 1);
        twoPlayerArrow.SetActive(selectedOption == 2);
    }

    void LoadMainScene()
    {
        PlayerPrefs.SetInt("PlayerMode", selectedOption);

        SceneManager.LoadScene("Main Scene");
    }
}
