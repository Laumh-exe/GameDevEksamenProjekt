using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    
    private HealthManager healthManager;

    private void Start(){
        healthManager = gameManager.GetComponent<HealthManager>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("DamageObject")) {
            healthManager.TakeDamage();
        }
    }
}
