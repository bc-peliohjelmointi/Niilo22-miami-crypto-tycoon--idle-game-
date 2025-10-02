using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    [SerializeField] private GameObject kauppaCanvas;

    // napit ja hinnat
    [SerializeField] private int kissaCost = 10;
    [SerializeField] private int autoCost = 50;
    [SerializeField] private int mukiCost = 20;
    [SerializeField] private int palmuCost = 5;

    private MoneySystem moneySystem;

    private bool kissaBought = false;
    private bool autoBought = false;
    private bool mukiBought = false;
    private bool palmuBought = false;

    private void Start()
    {
        //moneySystem = FindObjectOfType<MoneySystem>();
    }

    public void LoadKauppa()
    {
        kauppaCanvas.SetActive(true);
    }

    public void ExitKauppa()
    {
        kauppaCanvas.SetActive(false);
    }

    // kissa upgrade tuo passiivista tuottoa
    public void BuyKissa()
    {
        if (!kissaBought && moneySystem.SpendMoney(kissaCost))
        {
            kissaBought = true;
            moneySystem.AddPassiveIncome(0.2f); 
            
        }
    }

    // auto upgarde antaa crash shieldin
    public void BuyAuto()
    {
        if (!autoBought && moneySystem.SpendMoney(autoCost))
        {
            autoBought = true;
            moneySystem.EnableCrashShield(0.1f);
            Debug.Log("Ostit Auton");
        }
    }

    // 1.2x profit multiplier
    public void BuyMuki()
    {
        if (!mukiBought && moneySystem.SpendMoney(mukiCost))
        {
            mukiBought = true;
            moneySystem.SetProfitMultiplier(1.2f); // 1.2x profits
            Debug.Log("Ostit Muki");
        }
    }

    // kosmeettinen esine
    public void BuyPalmu()
    {
        if (!palmuBought && moneySystem.SpendMoney(palmuCost))
        {
            palmuBought = true;
            Debug.Log("Ostit Palmun");
        }
    }
}
