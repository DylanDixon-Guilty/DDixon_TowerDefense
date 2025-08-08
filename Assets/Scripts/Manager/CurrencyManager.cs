using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    public static int CurrentCurrency;
    public string Level01Scene;
    public string Level02Scene;
    public string Level03Scene;
    public string Level04Scene;

    private TextMeshProUGUI currencyScoreText;

    void Awake()
    {
        currencyScoreText = GetComponent<TextMeshProUGUI>();
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == Level01Scene)
        {
            CurrentCurrency = 200;
        }
        else if(currentScene.name == Level02Scene)
        {
            CurrentCurrency = 0;
        }
        else if(currentScene.name == Level03Scene)
        {
            CurrentCurrency = 0;
        }
        else if(currentScene.name == Level04Scene)
        {
            CurrentCurrency = 0;
        }

    }

    
    void Update()
    {
        currencyScoreText.text = "Blue Gold: " + CurrentCurrency;
    }
}
