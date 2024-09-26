using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour{
    private int baseHealth = 3;
    private int currentPlayerHealth;
    private bool isPlayerDead;

    private void Start(){
        InitPlayerHealth();
    }

    private void Update(){
        if (currentPlayerHealth <= 0 && !isPlayerDead) {
            StartCoroutine(KillPlayer());
        }
    }

    private void InitPlayerHealth(){
        currentPlayerHealth = 3;
        isPlayerDead = false;
        Debug.Log("Health: " + currentPlayerHealth);
    }

    public void TakeDamage(){
        if (!isPlayerDead) {
            currentPlayerHealth--;
            Debug.Log("Health: " + currentPlayerHealth);
        }
    }

    private IEnumerator KillPlayer(){
        isPlayerDead = true;
        Debug.Log("Player is dead, Restarting to 3 lives - Change later so game restarts or whatever you want to do");
        yield return new WaitForSeconds(1); // Add a delay of 1 second
        InitPlayerHealth();
    }
}