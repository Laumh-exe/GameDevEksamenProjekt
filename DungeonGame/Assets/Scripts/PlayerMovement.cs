using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour{
    [Header("CameraMovement")] 
    [SerializeField] private GameObject followTarget;
    [SerializeField] private float rotationPower = 3f;
    
    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float rotationLerp;

    [Header("Ground Check")] [SerializeField]
    private float playerHeight;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject playerModel;

    private bool grounded;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Vector3 rotateDirection;
    private Rigidbody rb;
    private PlayerInputs playerInputs;
    private Quaternion nextRotation;
    private Vector3 nextPosition;
    private Vector3 angles;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        playerInputs.CameraMovement.Enable();
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update(){
        Vector2 cameraMovement = playerInputs.CameraMovement.Move.ReadValue<Vector2>();
        VerticalCameraRotation(cameraMovement.x);
        HorizontalCameraRotation(cameraMovement.y);
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);
        SpeedControl();

        if (grounded) {
            rb.linearDamping = groundDrag;
        }
        else {
            rb.linearDamping = 0;
        }

        if (playerInputs.Player.Jump.triggered) {
            Jump();
        }
    }

    private void FixedUpdate(){
        Move();
    }

    private void Jump(){
        if (grounded) {
            rb.AddForce(playerModel.transform.up * jumpHeight, ForceMode.Impulse);
        }
    }

    private void Move(){
        //this.transform.position += new Vector3(getMovementVectorNormalized().x, 0, getMovementVectorNormalized().y) * moveSpeed * Time.deltaTime;
        horizontalInput = GetMovementVectorNormalized().x;
        verticalInput = GetMovementVectorNormalized().y;
        moveDirection = transform.forward * verticalInput;
        //rotateDirection = transform.right * horizontalInput;

        //const float rotateSpeed = 3f;
        //transform.forward = Vector3.Slerp(transform.forward, rotateDirection.normalized, Time.deltaTime * rotateSpeed);

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
    
    private void VerticalCameraRotation(float y){
        if (!Input.GetMouseButton(0)) {
            followTarget.transform.rotation *= Quaternion.AngleAxis(y * rotationPower, Vector3.up);
            ClampVerticalCameraRotation();
        }
        
    }

    private void HorizontalCameraRotation(float x){
        if (!Input.GetMouseButton(0)) {
            followTarget.transform.rotation *= Quaternion.AngleAxis(x * rotationPower, Vector3.right);
        }
    }
    
    private void ClampVerticalCameraRotation(){
        angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340) {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40) {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;
    }

    private void RotatePlayer(){
        nextRotation = Quaternion.Lerp(followTarget.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (horizontalInput == 0 && verticalInput == 0) 
        {   
            nextPosition = transform.position;

            if (Input.GetMouseButton(0)) // DOESNT WORK
            {
                Debug.Log("Mouse button is pressed");
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }

            return; 
        }
        

        //Set the player rotation based on the look transform
        var rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationLerp);
        //reset the y rotation of the look transform
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputs.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public float GetSpeed(){
        return moveSpeed;
    }

    public void SetSpeed(float speed){
        moveSpeed = speed;
    }

    public Vector3 GetMoveDirection(){
        return moveDirection;
    }

    private void OnDestroy(){
        playerInputs.Disable();
    }

    private void LateUpdate(){
        RotatePlayer();
    }
}