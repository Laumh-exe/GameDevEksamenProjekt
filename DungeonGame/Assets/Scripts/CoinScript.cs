using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Antallet af point, mønten giver

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Sørg for at spilleren har taggen "Player"
        {
            // Find ScoreManager og tilføj point
            FindObjectOfType<ScoreManager>().AddPoints(coinValue);

            // Fjern mønten fra scenen
            Destroy(gameObject);
        }
    }
}
