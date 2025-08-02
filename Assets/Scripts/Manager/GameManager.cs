using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    public static bool IsGamePaused = false;

    public Health playerHealth;
    public GameObject OptionsMenuScreenInGame;
    public GameObject MainHUDScreen;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !IsGamePaused)
        {
            IsGamePaused = true;
            Time.timeScale = 0f;
            MainHUDScreen.SetActive(false);
            OptionsMenuScreenInGame.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && IsGamePaused)
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
}
