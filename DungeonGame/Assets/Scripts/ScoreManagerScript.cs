using UnityEngine;
using UnityEngine.UI;  // Dette bruges til at opdatere UI-elementer

public class ScoreManagerScript : MonoBehaviour
{
    public int score = 0; // Dette holder styr på spillerens score
    public Text scoreText; // UI tekstfelt, som viser scoren

    // Denne funktion tilføjer point
    public void AddPoints(int points)
    {
        score += points; 
        Debug.Log(score);
        // Opdater scoren med de tilføjede point
       // UpdateScoreText();  // Opdater teksten på skærmen
    }

    // Denne funktion opdaterer score-teksten
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Opdaterer UI'en
    }
}
