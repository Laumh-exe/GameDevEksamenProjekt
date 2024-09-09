using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // V�rdi af m�nten
    public ScoreManager scoreManager;

    void OnTriggerEnter(Collider other)
    {
        // Tjekker om den genstand, der kolliderer med m�nten, er spilleren
        if (other.gameObject.CompareTag("Player"))
        {
            // Tilf�jer point til spillerens score
            scoreManager.AddPoints(coinValue);

            // �del�g m�nten efter at spilleren har samlet den op
            Destroy(this);
        }
    }
}
