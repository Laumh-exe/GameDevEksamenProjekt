using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour{
    [SerializeField] private int baseHealth = 3;
    private int currentPlayerHealth;
    private bool isPlayerDead;

    public Action OnDamageTaken;
    public Action OnDeath;

    private void Start()
    {
        currentPlayerHealth = baseHealth;
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
        Debug.Log("Health: " + currentPlayerHealth);
    }

    public void TakeDamage(){
        if (!isPlayerDead) {
            currentPlayerHealth--;
            Debug.Log("Health: " + currentPlayerHealth);

            SoundManager.instance.PlayDamageSound();

            OnDamageTaken?.Invoke();
        }
    }

    private IEnumerator KillPlayer(){
        isPlayerDead = true;
        Debug.Log("Player is dead, Restarting to 3 lives - Change later so game restarts or whatever you want to do");
        yield return new WaitForSeconds(1); // Add a delay of 1 second

        SoundManager.instance.PlayDeadSound();

        //InitPlayerHealth();
        OnDeath?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetMaxHealth()
    {
        return baseHealth;
    }

    public int GetCurrentHealth()
    {
        return currentPlayerHealth;
    }
}