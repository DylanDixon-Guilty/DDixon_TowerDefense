using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    public static int CurrentCurrency;

    [SerializeField] private TextMeshProUGUI currencyScoreText;
    [SerializeField] private string Level01Scene;
    [SerializeField] private string Level02Scene;
    [SerializeField] private string Level03Scene;
    [SerializeField] private string Level04Scene;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if(currentScene.name == Level01Scene)
        {
            CurrentCurrency = 200;
        }
        else if(currentScene.name == Level02Scene)
        {
            CurrentCurrency = 250;
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
