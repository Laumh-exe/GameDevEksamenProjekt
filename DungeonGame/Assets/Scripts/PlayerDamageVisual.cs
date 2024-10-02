using System;
using UnityEngine.Events;
using UnityEngine;

public class PlayerDamageVisual : MonoBehaviour
{
    [SerializeField] private GameObject playerDamaged;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private float colorOnTime = .5f;

    private bool isTimerRunning = false;
    private float timeRemaining;

    private void Start()
    {
        timeRemaining = colorOnTime;
        HealthManager healthManager = gameManager.GetComponent<HealthManager>();
        healthManager.OnDamageTaken += HandleDamageTaken;
    }

    private void HandleDamageTaken()
    {
        playerDamaged.gameObject.SetActive(true);
        isTimerRunning = true;
    }

    void Update()
    {
        CountdownTimer();
    }
    
    private void CountdownTimer()
    {
        if (isTimerRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;

            }
            else
            {
                playerDamaged.gameObject.SetActive(false);
                timeRemaining = colorOnTime;
                isTimerRunning = false;
            }
        }
    }
}
