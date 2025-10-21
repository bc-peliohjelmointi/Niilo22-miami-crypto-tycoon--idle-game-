using UnityEngine;

public class MoneyDebug : MonoBehaviour
{
    private MoneySystem moneySystem;

    void Start()
    {
        moneySystem = FindObjectOfType<MoneySystem>();
    }

    public void AddDebugMoney()
    {
        if (moneySystem != null)
        {
            moneySystem.AddMoney(100);
            Debug.Log("Added $100 via Debug Button");
        }
    }
}
