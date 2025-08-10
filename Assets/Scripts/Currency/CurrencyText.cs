using TMPro;
using UnityEngine;

public class CurrencyText : MonoBehaviour
{
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private TextMeshProUGUI currentCurrencyText;

    private void Start()
    {
        if(currentCurrencyText != null)
        {
            currencyManager.OnCurrencyChange += UpdateCurrentCurrency;
        }
    }

    private void UpdateCurrentCurrency(int currentCurrency)
    {
        currentCurrencyText.text = "Blue Gold: " + currentCurrency.ToString();
    }
}
