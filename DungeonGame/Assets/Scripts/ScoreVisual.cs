using UnityEngine;
using UnityEngine.UI;

public class ScoreVisual : MonoBehaviour{
    [SerializeField] private Text scoreText;
    
    void Start(){
        ScoreManager.Instance.OnPoint += UpdateScoreText;
    }

    void UpdateScoreText(){
        scoreText.text = "Score: " + ScoreManager.Instance.score;
    }
    
    void OnDestroy(){
        ScoreManager.Instance.OnPoint -= UpdateScoreText;
    }
}
