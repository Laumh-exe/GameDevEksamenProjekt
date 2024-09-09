using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Værdi af mønten
    public ScoreManager scoreManager;

    void OnTriggerEnter(Collider other)
    {
        // Tjekker om den genstand, der kolliderer med mønten, er spilleren
        if (other.gameObject.CompareTag("Player"))
        {
            // Tilføjer point til spillerens score
            scoreManager.AddPoints(coinValue);

            // Ødelæg mønten efter at spilleren har samlet den op
            Destroy(this);
        }
    }
}
