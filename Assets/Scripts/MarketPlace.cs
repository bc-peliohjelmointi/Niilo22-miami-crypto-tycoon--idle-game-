using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarketPlace : MonoBehaviour
{
    [SerializeField] private GameObject kauppaCanvas;

    
    [SerializeField] private Text kissaPriceText;
    [SerializeField] private Text autoPriceText;
    [SerializeField] private Text mukiPriceText;
    [SerializeField] private Text palmuPriceText;

    
    [SerializeField] private int kissaBaseCost = 10;
    [SerializeField] private int autoBaseCost = 500;
    [SerializeField] private int mukiBaseCost = 50;
    [SerializeField] private int palmuBaseCost = 5;

    private int kissaCost;
    private int autoCost;
    private int mukiCost;
    private int palmuCost;

    private int kissaLevel = 0;
    private int autoLevel = 0;
    private int mukiLevel = 0;
    private int palmuLevel = 0;

    private MoneySystem moneySystem;

    [SerializeField] private string newGameScene;

    private void Start()
    {
        moneySystem = FindFirstObjectByType<MoneySystem>();

        // Asetetaan lähtöhinnat
        kissaCost = kissaBaseCost;
        autoCost = autoBaseCost;
        mukiCost = mukiBaseCost;
        palmuCost = palmuBaseCost;

        UpdatePriceTexts();
    }

    public void LoadKauppa()
    {
        kauppaCanvas.SetActive(true);
        UpdatePriceTexts(); // päivitetään hinnat aina kun avataan
    }

    public void ExitKauppa()
    {
        kauppaCanvas.SetActive(false);
        SceneManager.LoadScene(newGameScene);
    }

    private void UpdatePriceTexts()
    {
        if (kissaPriceText) kissaPriceText.text = "$" + kissaCost;
        if (autoPriceText) autoPriceText.text = "$" + autoCost;
        if (mukiPriceText) mukiPriceText.text = "$" + mukiCost;
        if (palmuPriceText) palmuPriceText.text = "$" + palmuCost;
    }

    //Kissa upgrade = passiivinen tulonlähde
    public void BuyKissa()
    {
        if (moneySystem.SpendMoney(kissaCost))
        {
            kissaLevel++;
            moneySystem.AddPassiveIncome(1f);
            kissaCost = Mathf.RoundToInt(kissaCost * 1.5f);
            UpdatePriceTexts();

            Debug.Log($" kissan tason {kissaLevel}");
        }
    }

    // Auto upgrade = crash shield
    public void BuyAuto()
    {
        if (moneySystem.SpendMoney(autoCost))
        {
            autoLevel++;
            float shieldValue = 0.1f * autoLevel;
            moneySystem.EnableCrashShield(shieldValue);
            autoCost = Mathf.RoundToInt(autoCost * 2f);
            UpdatePriceTexts();

            Debug.Log($"Auto taso {autoLevel}");
        }
    }

    // Muki upgrade = profit multiplier
    public void BuyMuki()
    {
        if (moneySystem.SpendMoney(mukiCost))
        {
            mukiLevel++;
            float multiplier = 1f + (0.2f * mukiLevel);
            moneySystem.SetProfitMultiplier(multiplier);
            mukiCost = Mathf.RoundToInt(mukiCost * 1.8f);
            UpdatePriceTexts();

            Debug.Log($"Muki taso {mukiLevel}");
        }
    }

    // Palmu upgrade = kosmetiikka item
    public void BuyPalmu()
    {
        if (moneySystem.SpendMoney(palmuCost))
        {
            palmuLevel++;
            palmuCost = Mathf.RoundToInt(palmuCost * 2f);
            UpdatePriceTexts();

            Debug.Log($"Palmu taso {palmuLevel}");
        }
    }
}
