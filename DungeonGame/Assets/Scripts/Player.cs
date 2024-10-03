using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    void Start()
    {
        HealthManager healthManager = gameManager.GetComponent<HealthManager>();
        healthManager.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Destroy(this.gameObject);
    }
}
