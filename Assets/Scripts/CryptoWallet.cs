using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CryptoWallet : MonoBehaviour
{
    public MoneySystem moneySystem;   // Vedä MoneySystem Inspectorista
    public float cryptoHoldings = 0f; // Pelaajan omistama määrä
    public float currentPrice = 1f;   // Nykyinen hinta (teidän crash-koodista päivitetään tänne)

    // UI (valinnainen)
    public TextMeshProUGUI holdingsText;
    public TextMeshProUGUI statusText;

    // Osta crypto dollareilla
    public void BuyCrypto(float dollarsToInvest)
    {
        if (moneySystem == null) return;

        if (!moneySystem.CanSpend(Mathf.RoundToInt(dollarsToInvest)))
        {
            if (statusText) statusText.text = "Ei tarpeeksi rahaa ostoon!";
            return;
        }

        // Montako crypto-yksikköä saa
        float bought = dollarsToInvest / currentPrice;

        // Vähennetään rahat
        moneySystem.SpendMoney(Mathf.RoundToInt(dollarsToInvest));

        // Lisätään omistukseen
        cryptoHoldings += bought;

        if (statusText) statusText.text = $"Ostit {bought:F2} cryptoa (${dollarsToInvest}).";
        UpdateUI();
    }

    // Myy tietty määrä crypto-yksiköitä
    public void SellCrypto(float amountToSell)
    {
        if (amountToSell <= 0 || amountToSell > cryptoHoldings)
        {
            if (statusText) statusText.text = "Ei tarpeeksi cryptoa!";
            return;
        }

        // Tulot = määrä * hinta
        int revenue = Mathf.RoundToInt(amountToSell * currentPrice);

        // Päivitetään varanto ja rahat
        cryptoHoldings -= amountToSell;
        moneySystem.AddMoney(revenue);

        if (statusText) statusText.text = $"Myit {amountToSell:F2} cryptoa (${revenue}).";
        UpdateUI();
    }

    // Myy kaikki omistukset
    public void SellAll()
    {
        if (cryptoHoldings <= 0)
        {
            if (statusText) statusText.text = "Ei mitään myytävää!";
            return;
        }

        SellCrypto(cryptoHoldings);
    }

    void UpdateUI()
    {
        if (holdingsText) holdingsText.text = $"Omat: {cryptoHoldings:F2} cr";
    }
}
