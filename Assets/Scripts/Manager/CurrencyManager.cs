using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    public static int Currency;
    public string Level01Scene;

    private TextMeshProUGUI currencyScoreText;

    void Awake()
    {
        currencyScoreText = GetComponent<TextMeshProUGUI>();
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == Level01Scene)
        {
            Currency = 200;
        }
    }

    
    void Update()
    {
        currencyScoreText.text = "Blue Gold: " + Currency;
    }
}
