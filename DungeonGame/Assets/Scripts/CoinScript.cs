using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour{
    [SerializeField] private int coinValue = 1;
    bool isCollected = false;

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player") && !isCollected) {
            isCollected = true;
            Destroy(gameObject);
            ScoreManager.Instance.AddPoints(coinValue);
            SoundManager.Instance.PlayCoinCollectSound();
        }
    }
}