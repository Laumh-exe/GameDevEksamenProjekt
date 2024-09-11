using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private PlayerInputs playerInputs;
    [SerializeField] private float speed = 5f;
    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
    }

    private void Update(){
        Move();
    }

    private void Move(){
        this.transform.position += new Vector3(getMovementVectorNormalized().x, 0, getMovementVectorNormalized().y) * speed * Time.deltaTime;
    }
    
    private Vector2 getMovementVectorNormalized(){
        Vector2 inputVector = playerInputs.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
