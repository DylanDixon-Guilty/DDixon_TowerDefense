using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    public static int Currency;
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
            Currency = 200;
        }
        else if(currentScene.name == Level02Scene)
        {
            Currency = 0;
        }
        else if(currentScene.name == Level03Scene)
        {
            Currency = 0;
        }
        else if(currentScene.name == Level04Scene)
        {
            Currency = 0;
        }

    }

    
    void Update()
    {
        currencyScoreText.text = "Blue Gold: " + Currency;
    }
}
