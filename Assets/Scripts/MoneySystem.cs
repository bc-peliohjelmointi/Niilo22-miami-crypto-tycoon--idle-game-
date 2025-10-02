using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public int money = 100;          // Pelaajan käteinen alussa
    public Text moneyText;

    public float profitMultiplier = 1f;   // Alussa 1.0x
    public bool hasCrashShield = false;   // Onko crash shield ostettu
    private float crashShieldStrength = 0f;

    private float passiveIncome = 0f;     // $/sekunti
    private float passiveTimer = 0f;

    void Start()
    {
        UpdateMoneyUI();
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
        money += Mathf.RoundToInt(amount * profitMultiplier);
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
}
