using System;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour{
    [SerializeField] private GameObject playerPosition;
    private void Update(){
        this.transform.position = new Vector3(playerPosition.transform.position.x, 0, playerPosition.transform.position.z);
    }
}
