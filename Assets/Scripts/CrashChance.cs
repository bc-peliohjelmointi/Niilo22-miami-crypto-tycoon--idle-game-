using UnityEngine;
using System.Collections;

public class CrashChance : MonoBehaviour
{
    public GameObject rocket;
    public GameObject explosionPrefab;

    public int chance = 8;      // Chance as in 1 in (chance)
    private int crashNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CheckRocketCrashChance());
    }

    IEnumerator CheckRocketCrashChance()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Waits 1 second
            Debug.Log("1 second!");


            crashNumber = Random.Range(0, chance);

            if (crashNumber == 0)
            {
                Debug.Log("CRASH!");

                if (explosionPrefab != null )
                {
                    Instantiate(explosionPrefab, rocket.transform.position, Quaternion.identity);
                }

                Destroy(rocket);
                yield break;
            }
            else
            {
                Debug.Log("No crash!");
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
