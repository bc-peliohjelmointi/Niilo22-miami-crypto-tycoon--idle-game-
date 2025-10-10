using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private string newGameScene; // M‰‰rit‰ unityss‰ seuraavan scenen nimi

    // BG music
    public AudioSource backgroundSource;
    public AudioClip backgroundLoop;


    

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

    private void Start()
    {
        if (backgroundSource != null && backgroundLoop != null) // Background music
        {
            backgroundSource.clip = backgroundLoop;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }
    }
}
