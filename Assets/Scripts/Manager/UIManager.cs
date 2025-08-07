using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool IsGamePaused = false;
    public GameObject OptionsMenuScreenInGame;
    public GameObject MainHUDScreen;
    public GameObject ConfirmExitScreenInGame;
    public string BackToTitleScreen;

    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI gameOverTextMessage;
    [SerializeField] private TextMeshProUGUI retryOrNextLevelText;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private string goToNextLevel;

    private void Start()
    {
        MainHUDScreen.SetActive(true);
        OptionsMenuScreenInGame.SetActive(false);
        ConfirmExitScreenInGame.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    private void Update()
    {
        PlayerCompletedLevel();
        if (Input.GetKeyDown(KeyCode.Escape) && !IsGamePaused && !highScoreManager.hasLevelCompleted)
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
    /// If the Player won, show the "You Won" Text and the "Next Level" Button-Text.
    /// If the Player lost, show the "You Lost" Text and the "Retry Level" Button-Text
    /// </summary>
    private void GameOverText()
    {
        if (!playerHealth.IsDead())
        {
            gameOverTextMessage.text = "You Won!!";
            retryOrNextLevelText.text = "Next Level";
        }
        else
        {
            gameOverTextMessage.text = "You Lost!!";
            retryOrNextLevelText.text = "Retry Level";
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

    /// <summary>
    /// This checks to see if the player won the level, then go to the Next Level.
    /// If the player lost, Restart the current Level (On pressing the Retry/Next Level button)
    /// </summary>
    public void LevelButtonFunction()
    {
        if (!playerHealth.IsDead())
        {
            SceneManager.LoadScene(goToNextLevel);
        }
        else if (playerHealth.IsDead())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// When the player completes the level, as in, they won or lost, hide the MainHud and Options
    /// </summary>
    private void PlayerCompletedLevel()
    {
        if (playerHealth.IsDead() || highScoreManager.hasLevelCompleted)
        {
            GameOverText();
            GameOverScreen.SetActive(true);
            MainHUDScreen.SetActive(false);
            OptionsMenuScreenInGame.SetActive(false);
        }
    }
}
