using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrashManager : MonoBehaviour
{
    public GameObject rocket;
    public GameObject explosionPrefab;
    public MoneySystem moneySystem;

    public int chance = 8;     // 1 in "chance"
    private int crashNumber;

    private bool isRunning = false;   
    private float betAmount = 0f;     
    private Coroutine crashRoutine;

    // UI
    public TextMeshProUGUI statusText;
    public Button stopButton;
    public GameObject defaultSelectedButton;

    // Sound
    public AudioSource audioSource;
    public AudioClip[] crashSounds; 
    public AudioClip[] winSounds;

    public AudioSource backgroundSource;
    public AudioSource backgroundAmbienceSource;
    public AudioClip backgroundLoop;
    public AudioClip backgroundAmbienceLoop;


    void Start()
    {
        if (stopButton != null)
            stopButton.interactable = false;

        // Hide rocket until a round starts
        if (rocket != null)
            rocket.SetActive(false);

        if (backgroundSource != null && backgroundLoop != null)
        {
            backgroundSource.clip = backgroundLoop;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }

        if (backgroundAmbienceSource != null && backgroundAmbienceLoop != null)
        {
            backgroundAmbienceSource.clip = backgroundAmbienceLoop;
            backgroundAmbienceSource.loop = true;
            backgroundAmbienceSource.Play();
        }
        if (moneySystem == null)
        {
            moneySystem = FindObjectOfType<MoneySystem>();
            if (moneySystem == null)
            {
                Debug.LogWarning("⚠️ CrashManager could not find a MoneySystem in the scene!");
            }
        }
    }

    // Bet $5 button
    public void Bet5()
    {
        if (isRunning) return; 
        float fixedBet = 5f;

        if (!moneySystem.CanSpend((int)fixedBet))
        {
            if (statusText) statusText.text = "Not enough money to bet $5!";
            return;
        }

        moneySystem.SpendMoney((int)fixedBet);
        betAmount += fixedBet;

        if (statusText) statusText.text = "You placed: " + betAmount + "$ Press Start when ready!";
    }

    // Start button
    public void StartRound()
    {
        if (isRunning) return;
        if (betAmount <= 0f)
        {
            if (statusText) statusText.text = "You must place a bet first!";
            return;
        }

        moneySystem.roundMultiplier = moneySystem.baseProfitMultiplier;

        if (statusText) statusText.text = "Round started!";
        isRunning = true;

        if (stopButton != null)
            stopButton.interactable = true;

        if (rocket != null)
            rocket.SetActive(true); // Show rocket

        crashRoutine = StartCoroutine(CheckRocketCrashChance());
    }

    // Stop button (cash out)
    public void Stop()
    {
        if (!isRunning) return;

        float payout = betAmount * moneySystem.profitMultiplier;
        moneySystem.AddMoney(Mathf.RoundToInt(payout));

        if (statusText) statusText.text = "You won "+payout+"$"; // simplified

        PlayRandomSound(winSounds);

        EndRound();
    }

    IEnumerator CheckRocketCrashChance()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            crashNumber = Random.Range(0, chance);

            if (crashNumber == 0)
            {
                if (moneySystem.hasCrashShield)
                {
                    float chanceToSurvive = moneySystem.GetCrashShieldStrength(); // e.g., 0.1 = 10%
                    if (Random.value < chanceToSurvive)
                    {
                        // Shield saves the round, maybe reduce shield strength
                        Debug.Log("Crash avoided by shield!");
                        continue; // skip crash this time
                    }
                }

                if (explosionPrefab != null && rocket != null)
                    Instantiate(explosionPrefab, rocket.transform.position, Quaternion.identity);

                if (statusText) statusText.text = "CRASH! You lost your money.";
                moneySystem.SetProfitMultiplier(1f);

                PlayRandomSound(crashSounds);

                EndRound();
                yield break;
            }
            else
            {
                moneySystem.SetProfitMultiplier(moneySystem.profitMultiplier + moneySystem.multiplierAmount);
            }
        }
    }

    void EndRound()
    {
        if (crashRoutine != null)
            StopCoroutine(crashRoutine);

        crashRoutine = null;
        isRunning = false;
        betAmount = 0f;

        moneySystem.SetProfitMultiplier(moneySystem.baseProfitMultiplier);

        if (stopButton != null)
            stopButton.interactable = false;

        if (rocket != null)
            rocket.SetActive(false); // Hide rocket after round ends

        StartCoroutine(SelectDefaultButtonNextFrame());
    }

    IEnumerator SelectDefaultButtonNextFrame()
    {
        yield return null; // odota 1 frame, jotta UI ehtii aktivoitua
        if (defaultSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
        }
    }

    void PlayRandomSound(AudioClip[] clips)
    {
        if (clips.Length == 0 || audioSource == null)
            return;

        int index = Random.Range(0, clips.Length);
        audioSource.clip = clips[index];
        audioSource.Play();
    }

    public void ReturnToShop()
    {
        SceneManager.LoadScene("Juuso");
    }
}
