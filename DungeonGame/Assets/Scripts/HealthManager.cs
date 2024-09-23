using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private const int baseHealth = 3;
    public int currentPlayerHealth;

    private void Start()
    {
        InitPlayerHealth();
    }

    private void InitPlayerHealth()
    {
        currentPlayerHealth = baseHealth;
    }

    private void TakeDamage()
    {
        currentPlayerHealth--;
    }

    private void KillPlayer()
    {
        if (currentPlayerHealth == 0)
        {
            Debug.Log("Player is dead, Restarting to 3 lives - Change later so Player respawns");
            InitPlayerHealth();
        }
    }
}