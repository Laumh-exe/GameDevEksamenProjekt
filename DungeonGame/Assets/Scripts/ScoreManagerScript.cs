using UnityEngine;
using UnityEngine.UI;  // Dette bruges til at opdatere UI-elementer

public class ScoreManagerScript : MonoBehaviour
{
    public int score = 0; // Dette holder styr p� spillerens score
    public Text scoreText; // UI tekstfelt, som viser scoren

    // Denne funktion tilf�jer point
    public void AddPoints(int points)
    {
        score += points; 
        Debug.Log(score);
        // Opdater scoren med de tilf�jede point
       // UpdateScoreText();  // Opdater teksten p� sk�rmen
    }

    // Denne funktion opdaterer score-teksten
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Opdaterer UI'en
    }
}
