using UnityEngine;
using UnityEngine.UI;

public class CryptoUI : MonoBehaviour
{
    public CryptoWallet wallet;

    public Button buyButton;
    public Button sellButton;
    public Button sellAllButton;

    void Start()
    {
        // Aseta napit kutsumaan funktioita
        buyButton.onClick.AddListener(() => wallet.BuyCrypto(20f));   // Osta aina 20$
        sellButton.onClick.AddListener(() => wallet.SellCrypto(1f)); // Myy 1 crypto
        sellAllButton.onClick.AddListener(() => wallet.SellAll());   // Myy kaikki
    }
}

