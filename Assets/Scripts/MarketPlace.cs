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
    private bool isPalmuBought;

    private MoneySystem moneySystem;

    [SerializeField] private string newGameScene;

    // Audios
    public AudioSource audioSource;
    public AudioClip kissaAudio;
    public AudioClip autoAudio;
    public AudioClip mukiAudio;
    public AudioClip palmuAudio;

    // BG music
    public AudioSource backgroundSource;
    public AudioClip backgroundLoop;

    private void Start()
    {
        moneySystem = FindFirstObjectByType<MoneySystem>();

        // Asettaa ja päivittää hinnat
        kissaCost = Mathf.RoundToInt(kissaBaseCost * Mathf.Pow(1.5f, moneySystem.kissaLevel));
        autoCost = Mathf.RoundToInt(autoBaseCost * Mathf.Pow(2f, moneySystem.autoLevel));
        mukiCost = Mathf.RoundToInt(mukiBaseCost * Mathf.Pow(1.8f, moneySystem.mukiLevel));
        palmuCost = Mathf.RoundToInt(palmuBaseCost * Mathf.Pow(2f, moneySystem.palmuLevel));

        UpdatePriceTexts();

        if (backgroundSource != null && backgroundLoop != null) // Background music
        {
            backgroundSource.clip = backgroundLoop;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
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
            moneySystem.kissaLevel++;
            moneySystem.AddPassiveIncome(1f);
            kissaCost = Mathf.RoundToInt(kissaCost * 1.5f);
            UpdatePriceTexts();

            audioSource.PlayOneShot(kissaAudio);
            Debug.Log($"Kissa taso {moneySystem.kissaLevel}");
        }
    }

    // Auto upgrade = crash shield
    public void BuyAuto()
    {
        if (moneySystem.SpendMoney(autoCost))
        {
            moneySystem.autoLevel++;
            float shieldValue = 0.1f * moneySystem.autoLevel;
            moneySystem.EnableCrashShield(shieldValue);
            autoCost = Mathf.RoundToInt(autoCost * 2f);
            UpdatePriceTexts();

            audioSource.PlayOneShot(autoAudio);
            Debug.Log($"Auto taso {moneySystem.autoLevel}");
        }
    }

    // Muki upgrade = profit multiplier
    public void BuyMuki()
    {
        if (moneySystem.SpendMoney(mukiCost))
        {
            moneySystem.mukiLevel++;
            float multiplier = 1f + (0.2f * moneySystem.mukiLevel);
            moneySystem.SetProfitMultiplier(multiplier);
            mukiCost = Mathf.RoundToInt(mukiCost * 1.8f);
            UpdatePriceTexts();

            audioSource.PlayOneShot(mukiAudio);
            Debug.Log($"Muki taso {moneySystem.mukiLevel}");
        }
    }

    // Palmu upgrade = kosmetiikka item
    public void BuyPalmu()
    {
        if (moneySystem.SpendMoney(palmuCost))
        {
            moneySystem.palmuLevel++;
            palmuCost = Mathf.RoundToInt(palmuCost * 2f);

            moneySystem.isPalmuUnlocked = true; // Store unlock permanently

            // Save this if you use PlayerPrefs for long-term persistence
            PlayerPrefs.SetInt("PalmuUnlocked", 1);
            PlayerPrefs.Save();


            UpdatePriceTexts();

            audioSource.PlayOneShot(palmuAudio);
            Debug.Log($"Palmu taso {moneySystem.palmuLevel}");
        }
    }
}
