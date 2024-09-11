using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // V�rdi af m�nten
    [SerializeField] private ScoreManagerScript scoreManager;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        // Tjekker om den genstand, der kolliderer med m�nten, er spilleren
        if (other.gameObject.CompareTag("Player"))
        {
            // Tilf�jer point til spillerens score
            scoreManager.AddPoints(coinValue);
            Debug.Log("Hit player");
            // �del�g m�nten efter at spilleren har samlet den op
            Destroy(gameObject);
        }
    }
}
