using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{
    public static ScoreManager Instance;
    public int score;

    public Action OnPoint;

    private void Start(){
        HealthManager.Instance.OnDeath += ResetScore;
    }

    void Awake(){
        score = 0;
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points){
        score += points;
        OnPoint?.Invoke();
    }
    
    private void ResetScore(){
        score = 0;
    }
    
    private void OnDestroy(){
        HealthManager.Instance.OnDeath -= ResetScore;
    }
}