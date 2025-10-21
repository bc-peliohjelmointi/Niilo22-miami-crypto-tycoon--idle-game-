using UnityEngine;

public class PalmuActivator : MonoBehaviour
{
    [SerializeField] private GameObject palmuImage; // vedä tähän palmun kuva (esim. PNG UI Image)

    void Start()
    {
        // Tarkistetaan, onko palmu ostettu
        if (PlayerPrefs.GetInt("PalmuUnlocked", 0) == 1)
        {
            if (palmuImage != null)
            {
                palmuImage.SetActive(true);
                
            }
        }
        else
        {
            if (palmuImage != null)
                palmuImage.SetActive(false);
        }
    }
}
