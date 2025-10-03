using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string newGameScene; // M‰‰rit‰ unityss‰ seuraavan scenen nimi

    // Lataa uuden scenen
    public void NewGame()
    {

        SceneManager.LoadScene(newGameScene);
    }

    // Sulkee pelin nappia painaessa
    public void Exit()
    {

        Application.Quit();
    }
}
