using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuManager : MonoBehaviour
{
    public string sceneToStartGame;
    public GameObject TitleScreen;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject ConfirmExitScreen;

    private void Awake()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        ConfirmExitScreen.SetActive(false);
    }

    /// <summary>
    /// On Pressing "Start Game" button, go to Level One Scene
    /// </summary>
    public void GoToLevelOne()
    {
        SceneManager.LoadScene(sceneToStartGame);
    }

    /// <summary>
    /// On Pressing "Press To Start" button, go to Main Menu Screen
    /// </summary>
    public void GoToMainMenu()
    {
        TitleScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// On Pressing "Options" button, go to Options Screen
    /// </summary>
    public void GoToOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    /// <summary>
    /// On Pressing "Back" button in the Options screen, go to MainMenu Screen
    /// </summary>
    public void BackToMainMenuFromOptions()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// On Pressing "Exit" button in the MainMenu Screen, go to ConfirmExit Screen
    /// </summary>
    public void GoToConfirmExit()
    {
        MainMenu.SetActive(false);
        ConfirmExitScreen.SetActive(true);
    }

    /// <summary>
    /// On Pressing "No" button from the ConfirmExit Screen, go to MainMenu Screen.
    /// </summary>
    public void BackToMainMenuFromConfirmExit()
    {
        ConfirmExitScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// On Pressing "Yes" button from the ConfirmExit Screen, Exit Game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
