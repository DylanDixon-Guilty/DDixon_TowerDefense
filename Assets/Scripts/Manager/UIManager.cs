using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject OptionsMenuScreenInGame;
    public GameObject MainHUDScreen;
    public GameObject ConfirmExitScreenInGame;
    public string BackToTitleScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsGamePaused)
        {
            IsGamePaused = true;
            Time.timeScale = 0f;
            MainHUDScreen.SetActive(false);
            OptionsMenuScreenInGame.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsGamePaused)
        {
            IsGamePaused = false;
            Time.timeScale = 1f;
            MainHUDScreen.SetActive(true);
            OptionsMenuScreenInGame.SetActive(false);
        }
    }

    /// <summary>
    /// On Pressing "Back" button while on the Options Screen on any level, go back to the game. 
    /// </summary>
    public void BackToGame()
    {
        if (IsGamePaused)
        {
            IsGamePaused = false;
            Time.timeScale = 1f;
            MainHUDScreen.SetActive(true);
            OptionsMenuScreenInGame.SetActive(false);
        }
    }

    /// <summary>
    /// On Pressing "Back To Main Menu" button while in any level, go to ConfirmExitScreen
    /// </summary>
    public void GoToConfirmExitInGame()
    {
        OptionsMenuScreenInGame.SetActive(false);
        ConfirmExitScreenInGame.SetActive(true);
    }

    /// <summary>
    /// On Pressing "No" button, go to Options Screen
    /// </summary>
    public void GoBackToOptions()
    {
        OptionsMenuScreenInGame.SetActive(true);
        ConfirmExitScreenInGame.SetActive(false);
    }

    /// <summary>
    /// On Pressing "Yes" button, go to TitleScreen Scene
    /// </summary>
    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(BackToTitleScreen);
    }
}
