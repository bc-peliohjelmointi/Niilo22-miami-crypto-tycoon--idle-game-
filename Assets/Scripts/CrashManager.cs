using UnityEngine;
using System.Collections;
using TMPro;
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

    // Sound
    public AudioSource loseAudio;

    void Start()
    {
        if (stopButton != null)
            stopButton.interactable = false;

        // Hide rocket until a round starts
        if (rocket != null)
            rocket.SetActive(false);
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

        moneySystem.SetProfitMultiplier(1f);

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

        if (statusText) statusText.text = "You won!"; // simplified

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
                if (explosionPrefab != null && rocket != null)
                    Instantiate(explosionPrefab, rocket.transform.position, Quaternion.identity);

                if (statusText) statusText.text = "CRASH! You lost your money.";
                moneySystem.SetProfitMultiplier(1f);

                loseAudio.Play();

                EndRound();
                yield break;
            }
            else
            {
                moneySystem.SetProfitMultiplier(moneySystem.profitMultiplier + 0.25f);
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

        if (stopButton != null)
            stopButton.interactable = false;

        if (rocket != null)
            rocket.SetActive(false); // Hide rocket after round ends
    }
}
