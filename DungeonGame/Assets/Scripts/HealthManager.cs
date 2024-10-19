using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour{
    [SerializeField] private int baseHealth;
    public static HealthManager Instance;
    private int currentPlayerHealth;
    private bool isPlayerDead;

    public Action OnDamageTaken;
    public Action OnDeath;
    
    void Awake(){
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
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
    }

    public void TakeDamage(){
        if (!isPlayerDead) {
            currentPlayerHealth--;

            SoundManager.Instance.PlayDamageSound();

            OnDamageTaken?.Invoke();
        }
    }

    private IEnumerator KillPlayer(){
        isPlayerDead = true;
        yield return new WaitForSeconds(1);

        SoundManager.Instance.PlayDeadSound();

        OnDeath?.Invoke();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
        yield return null; 
        InitPlayerHealth();
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