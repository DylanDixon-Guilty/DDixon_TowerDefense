using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuManager : MonoBehaviour
{
    public string sceneToStartGame;
    public GameObject TitleScreen;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject ConfirmExitScreen;

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

    public void BackToMainMenu()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
}
