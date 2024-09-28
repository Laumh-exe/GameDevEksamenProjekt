using UnityEngine;
using UnityEngine.UI; // Dette bruges til at arbejde med UI-elementer

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Spillerens score
    public Text scoreText; // Referencen til UI tekstfeltet

    // Funktion til at tilføje point
    public void AddPoints(int points)
    {
        score += points; // Tilføjer point til den samlede score
        UpdateScoreText(); // Opdaterer UI-teksten
    }

    // Opdaterer score UI-teksten
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Viser scoren på skærmen
    }
}