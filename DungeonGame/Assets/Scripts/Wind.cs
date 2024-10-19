using System;
using UnityEngine;

public class Wind : MonoBehaviour{
    private GameObject player;
    [SerializeField] private float windForce;
    

    private void OnTriggerEnter(Collider other){
        Debug.Log("Collided with wind");
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Applying wind force to player");
            Rigidbody rb = other.transform.parent.GetComponent<Rigidbody>();
            Vector3 force = this.transform.forward * windForce;
            Debug.Log("Force vector: " + force);
            rb.AddForce(force, ForceMode.Impulse);
        }
        

    }
}