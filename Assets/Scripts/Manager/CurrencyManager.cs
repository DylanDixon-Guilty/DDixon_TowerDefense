using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static int Currency;

    private TextMeshProUGUI currencyScoreText;

    void Awake()
    {
        currencyScoreText = GetComponent<TextMeshProUGUI>();
        Currency = 0;
    }

    
    void Update()
    {
        currencyScoreText.text = "Blue Gold: " + Currency;
    }
}
