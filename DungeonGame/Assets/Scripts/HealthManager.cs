using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour{
    private int baseHealth = 3;
    private int currentPlayerHealth;
    private bool isPlayerDead;

    public Health healthUI; // Reference til Health scriptet

    private void Start(){
        InitPlayerHealth();
    }

    private void Update(){
        if (currentPlayerHealth <= 0 && !isPlayerDead) {
            StartCoroutine(KillPlayer());
        }
    }

    private void InitPlayerHealth(){
        currentPlayerHealth = baseHealth;
        isPlayerDead = false;
        UpdateHealthUI(); // Opdaterer UI med spillerens helbred
    }

    public void TakeDamage(){
        if (!isPlayerDead) {
            currentPlayerHealth--;
            Debug.Log("Health: " + currentPlayerHealth);
            UpdateHealthUI(); // Opdaterer UI når spilleren tager skade
        }
    }

    private IEnumerator KillPlayer(){
        isPlayerDead = true;
        Debug.Log("Player is dead, Restarting to 3 lives - Change later so game restarts or whatever you want to do");
        yield return new WaitForSeconds(1); // Add a delay of 1 second
        InitPlayerHealth();
    }

    // Opdaterer UI
    private void UpdateHealthUI(){
        if (healthUI != null) {
            healthUI.health = currentPlayerHealth; // Opdaterer Health scriptet
        }
    }
}
