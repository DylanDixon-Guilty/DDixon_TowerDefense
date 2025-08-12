using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI gameOverTextMessage;
    [SerializeField] private TextMeshProUGUI retryOrNextLevelText;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] GameObject OptionsMenuScreenInGame;
    [SerializeField] GameObject MainHUDScreen;
    [SerializeField] GameObject ConfirmExitScreenInGame;
    [SerializeField] private string goToNextLevel;
    [SerializeField] private string BackToTitleScreen;
    [SerializeField] private string finalLevel;
    private string level01 = "Level 01";
    private string wonText = "You Won!!";
    private string lostText = "You Lost!!";
    private string wonFinalLevelText = "You Have Defeated The Blue Minions!!!";
    private string nextLevelText = "Next Level";
    private string retryLevelText = "Retry Level";
    private string restartGameText = "Restart Game";
    private bool isLevelConcludedButton = false;
    private bool isExitingGame = false;
    private bool IsGamePaused = false;

    private void Start()
    {
        
        MainHUDScreen.SetActive(true);
        OptionsMenuScreenInGame.SetActive(false);
        ConfirmExitScreenInGame.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsGamePaused && !gameManager.hasLevelConcluded)
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

        if(gameManager.hasLevelConcluded)
        {
            PlayerConcludedLevel();
        }
    }

    /// <summary>
    /// If the Player won, show the "You Won" Text and the "Next Level" Button-Text.
    /// If the Player lost, show the "You Lost" Text and the "Retry Level" Button-Text
    /// </summary>
    private void GameOverText()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == finalLevel && !playerHealth.IsDead())
        {
            gameOverTextMessage.text = wonFinalLevelText;
            retryOrNextLevelText.text = restartGameText;
        }
        else if(!playerHealth.IsDead())
        {
            gameOverTextMessage.text = wonText;
            retryOrNextLevelText.text = nextLevelText;
        }
        else
        {
            gameOverTextMessage.text = lostText;
            retryOrNextLevelText.text = retryLevelText;
        }
    }

    /// <summary>
    /// On Pressing "Back" button while on the Options Screen on any level, go back to the game. 
    /// </summary>
    public void BackToGame()
    {
        if(IsGamePaused)
        {
            IsGamePaused = false;
            Time.timeScale = 1f;
            MainHUDScreen.SetActive(true);
            OptionsMenuScreenInGame.SetActive(false);
        }
    }

    /// <summary>
    /// On Pressing "Return To Main Menu" button while in any level, go to ConfirmExitScreen
    /// </summary>
    public void GoToConfirmExitInGame()
    {
        if(gameManager.hasLevelConcluded)
        {
            isLevelConcludedButton = true;
            GameOverScreen.SetActive(false);
            ConfirmExitScreenInGame.SetActive(true);
        }
        else
        {
            isLevelConcludedButton = false;
            OptionsMenuScreenInGame.SetActive(false);
            ConfirmExitScreenInGame.SetActive(true);
        }
    }

    /// <summary>
    /// On Pressing "Quit Game" button, go to ConfirmExitScreen
    /// </summary>
    public void GoToExitGameScreen()
    {
        isExitingGame = true;
        GameOverScreen.SetActive(false);
        ConfirmExitScreenInGame.SetActive(true);
    }

    /// <summary>
    /// On Pressing "No" button, go to either Options Screen or GameOverScreen
    /// </summary>
    public void GoBackToPreviousScreen()
    {
        if(isExitingGame || isLevelConcludedButton)
        {
            isExitingGame = false;
            isLevelConcludedButton = false;
            ConfirmExitScreenInGame.SetActive(false);
            GameOverScreen.SetActive(true);
        }
        else
        {
            OptionsMenuScreenInGame.SetActive(true);
            ConfirmExitScreenInGame.SetActive(false);
        }
    }

    /// <summary>
    /// On Pressing "Yes" button, either go to TitleScreen Scene or Exit the Game
    /// </summary>
    public void GoToTitleScreenOrExitGame()
    {
        if(isLevelConcludedButton || !isExitingGame)
        {
            SceneManager.LoadScene(BackToTitleScreen);
        }
        else if(isExitingGame && !isLevelConcludedButton)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// This checks to see if the player won the level, then go to the Next Level.
    /// If the player lost, Restart the current Level (On pressing the Retry/Next Level button).
    /// If the player won the Final Level, give the option to restart at Level 01
    /// </summary>
    public void RetryOrNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == finalLevel && !playerHealth.IsDead())
        {
            SceneManager.LoadScene(level01);
        }
        else if(!playerHealth.IsDead())
        {
            SceneManager.LoadScene(goToNextLevel);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// When the player completes the level, as in, they won or lost, hide the MainHud and Options
    /// </summary>
    private void PlayerConcludedLevel()
    {
        
        if(!isLevelConcludedButton && !isExitingGame)
        {
            GameOverText();
            GameOverScreen.SetActive(true);
            MainHUDScreen.SetActive(false);
            OptionsMenuScreenInGame.SetActive(false);
        }
    }
}
