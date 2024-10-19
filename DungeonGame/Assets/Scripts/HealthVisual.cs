using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour{
    private int currentHealth;
    private int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private HealthManager healthManager;

    private void Start(){
        healthManager = HealthManager.Instance;
        numOfHearts = healthManager.GetMaxHealth();
    }

    void Update(){
        currentHealth = healthManager.GetCurrentHealth();


        if (currentHealth > numOfHearts) {
            currentHealth = numOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentHealth) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }


            hearts[i].enabled = i < numOfHearts;
        }
    }
}