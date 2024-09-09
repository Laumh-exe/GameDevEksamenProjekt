using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // V�rdi af m�nten

    void OnTriggerEnter(Collider other)
    {
        // Tjekker om den genstand, der kolliderer med m�nten, er spilleren
        if (other.gameObject.CompareTag("Player"))
        {
            // Tilf�jer point til spillerens score
            FindObjectOfType<ScoreManager>().AddPoints(coinValue);

            // �del�g m�nten efter at spilleren har samlet den op
            Destroy(gameObject);
        }
    }
}
