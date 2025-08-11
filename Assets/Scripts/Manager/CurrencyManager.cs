using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    public static int CurrentCurrency;

    [SerializeField] private TextMeshProUGUI currencyScoreText;
    [SerializeField] private TextMeshProUGUI balistaTowerText;
    [SerializeField] private TextMeshProUGUI cannonTowerText;
    [SerializeField] private TextMeshProUGUI freezeTowerText;
    private const string level01 = "Level 01";
    private const string level02 = "Level 02";
    private const string level03 = "Level 03";
    private const string level04 = "Level 04";
    private int costOfBalistaTower = 70;
    private int costOfCannonTower = 100;
    private int costOfFreezeTower = 120;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        switch(currentScene.name)
        {
            case level01:
                CurrentCurrency = 200;
                break;
            case level02:
                CurrentCurrency = 250;
                cannonTowerText.text = "Cost: " + costOfCannonTower;

                break;
            case level03:
                CurrentCurrency = 310;
                cannonTowerText.text = "Cost: " + costOfCannonTower;

                break;
            case level04:
                CurrentCurrency = 400;
                cannonTowerText.text = "Cost: " + costOfCannonTower;
                freezeTowerText.text = "Cost: " + costOfFreezeTower;
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        balistaTowerText.text = "Cost: " + costOfBalistaTower;
    }
    
    private void Update()
    {
        currencyScoreText.text = "Blue Gold: " + CurrentCurrency;
    }
}
