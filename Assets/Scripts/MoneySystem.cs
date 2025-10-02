using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    public int money = 100;         // Pelaajan käteinen alussa
    public Text moneyText;         // UI-kenttä inspectorissa

    void Start()
    {
        UpdateMoneyUI();
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

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Raha: $" + money;
    }
}
