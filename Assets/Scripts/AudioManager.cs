using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;   // asetetaan Inspectorissa
    public AudioClip Sound;        

    private void Awake()
    {
        // Singleton, varmistetaan ett‰ on vain yksi AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // s‰ilyy scenejen v‰lill‰
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Soita taustamusiikki kun peli alkaa
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    // soittaa musiikkia
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // soittaa ‰‰niefektej‰
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
