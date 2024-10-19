using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        HealthManager.Instance.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Destroy(this.gameObject);
    }
    
    private void OnDestroy()
    {
        HealthManager.Instance.OnDeath -= HandleDeath;
    }
}
