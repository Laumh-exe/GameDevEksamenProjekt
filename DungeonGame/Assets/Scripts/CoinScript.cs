using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Værdi af mønten
    [SerializeField] private ScoreManagerScript scoreManager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        // Tjekker om den genstand, der kolliderer med mønten, er spilleren
        if (other.gameObject.CompareTag("Player"))
        {
            // Tilføjer point til spillerens score
            scoreManager.AddPoints(coinValue);
            Debug.Log("Hit player");
            // Ødelæg mønten efter at spilleren har samlet den op
            Destroy(gameObject);
        }
    }
}
