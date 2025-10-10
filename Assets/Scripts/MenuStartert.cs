using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuStarter : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;

    void OnEnable()
    {
        StartCoroutine(SelectWithDelay());
    }

    IEnumerator SelectWithDelay()
    {
        yield return null; // odota 1 frame
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}
