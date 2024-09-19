using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Antallet af point, m�nten giver

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // S�rg for at spilleren har taggen "Player"
        {
            // Find ScoreManager og tilf�j point
            FindObjectOfType<ScoreManager>().AddPoints(coinValue);

            // Fjern m�nten fra scenen
            Destroy(gameObject);
        }
    }
}
