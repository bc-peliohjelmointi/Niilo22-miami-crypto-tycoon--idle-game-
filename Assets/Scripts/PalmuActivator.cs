using UnityEngine;

public class PalmuActivator : MonoBehaviour
{
    [SerializeField] private GameObject palmuImage;
    private MoneySystem moneySystem;

    void Awake()
    {
        // Hide immediately so no flicker
        if (palmuImage != null)
            palmuImage.SetActive(false);
    }

    void Start()
    {
        moneySystem = FindFirstObjectByType<MoneySystem>();
        if (moneySystem == null || palmuImage == null) return;

        // Show if unlocked
        palmuImage.SetActive(moneySystem.isPalmuUnlocked);
    }
}
