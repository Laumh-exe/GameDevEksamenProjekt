using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private int currentHealth;
    private int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private HealthManager healthManager;

    private void Start()
    {
        healthManager = gameManager.GetComponent<HealthManager>();
        numOfHearts = healthManager.GetMaxHealth();

    }

    void Update()
    {
        currentHealth = healthManager.GetCurrentHealth();
        
        // Ensure health doesn't exceed the number of hearts
        if (currentHealth > numOfHearts)
        {
            currentHealth = numOfHearts;
        }

        // Update heart display
        for (int i = 0; i < hearts.Length; i++)
        {
            //Debug.Log("Current Health: " + currentHealth + ", Number of Hearts: " + numOfHearts + ", i: " + i + ", Hearts Length: " + hearts.Length + ", Hearts[i]: " + hearts[i]);
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Toggle heart visibility based on the number of hearts
            hearts[i].enabled = i < numOfHearts;
        }
    }
}
