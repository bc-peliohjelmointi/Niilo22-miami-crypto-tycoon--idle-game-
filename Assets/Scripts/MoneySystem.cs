using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoneySystem : MonoBehaviour
{
    public int money = 100;          // Pelaajan käteinen alussa
    public TextMeshProUGUI moneyText;

    public float profitMultiplier = 1f;   // Alussa 1.0x
    public TextMeshProUGUI multiplierText;

    public bool hasCrashShield = false;   // Onko crash shield ostettu
    private float crashShieldStrength = 0f;

    private float passiveIncome = 0f;     // $/sekunti
    private float passiveTimer = 0f;

    public float baseProfitMultiplier = 1f; // from upgrades
    public float roundMultiplier = 1f;      // changes during round


    void Awake()
    {
        MoneySystem[] systems = FindObjectsOfType<MoneySystem>();
        if (systems.Length > 1)
        {
            Debug.Log("jghjgjh");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe when destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find new UI elements in the scene
        moneyText = GameObject.FindWithTag("MoneyText")?.GetComponent<TextMeshProUGUI>();
        multiplierText = GameObject.FindWithTag("MultiplierText")?.GetComponent<TextMeshProUGUI>();

        // Update UI if found
        UpdateMoneyUI();
        UpdateMultiplierUI();
    }

    void Start()
    {
#if UNITY_EDITOR
       
        PlayerPrefs.SetInt("PalmuUnlocked", 0);
        PlayerPrefs.Save();
#endif

        UpdateMoneyUI();
        UpdateMultiplierUI();
    }

    void Update()
    {
        // passiivitulo laskuri
        if (passiveIncome > 0)
        {
            passiveTimer += Time.deltaTime;
            if (passiveTimer >= 1f)
            {
                money += Mathf.RoundToInt(passiveIncome);
                passiveTimer = 0f;
                UpdateMoneyUI();
            }
        }
    }

    public bool CanSpend(int amount)
    {
        return money >= amount;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
            return true;
        }
        return false;
    }

    public void AddPassiveIncome(float amount)
    {
        passiveIncome += amount;
    }

    public void SetProfitMultiplier(float multiplier)
    {
        profitMultiplier = multiplier;
        UpdateMultiplierUI();
    }

    public void EnableCrashShield(float strength)
    {
        hasCrashShield = true;
        crashShieldStrength = strength; // esim. 0.1 = 10%
    }

    public float GetCrashShieldStrength()
    {
        return crashShieldStrength;
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Raha: $" + money;
    }

    void UpdateMultiplierUI()
    {
        if (multiplierText != null)
            multiplierText.text = "x " + profitMultiplier.ToString("0.00");
    }
}
