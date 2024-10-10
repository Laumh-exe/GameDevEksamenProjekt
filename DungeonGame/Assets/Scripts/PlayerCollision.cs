using System;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerCollision : MonoBehaviour
{
    private HealthManager healthManager;

    private void Start(){
        healthManager = HealthManager.Instance;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("DamageObject")) {
            healthManager.TakeDamage();
        }
    }
}
