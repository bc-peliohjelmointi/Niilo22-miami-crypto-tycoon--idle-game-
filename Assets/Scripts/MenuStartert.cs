using UnityEngine;
using UnityEngine.EventSystems;

public class MenuStarter : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;

    void OnEnable()
    {
        // Kun menu aktivoidaan, ohjain saa heti fokuksen ekaan nappiin
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}
