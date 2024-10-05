using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour{
    [Header("CameraMovement")] [SerializeField]
    private Transform followTarget;
    private int minXCamClamp = -70;
    private int maxXCamClamp = 70;
    [SerializeField] private float rotationPower = .3f;
    [SerializeField] private GameObject mainCamera;
    

    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpHeight;

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
    private Vector2 lookInput;
    private float camYRotation;
    private float camXRotation;
    private float clickInput;
    private float cameraFollowSpeed = 5f;

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

        MouseLookInput();
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
        horizontalInput = GetMovementVectorNormalized().x;
        verticalInput = GetMovementVectorNormalized().y;
        
        moveDirection = transform.forward * verticalInput;
        rotateDirection = transform.right * horizontalInput;

        
        const float rotateSpeed = 3f;
        transform.forward = Vector3.Slerp(transform.forward, rotateDirection.normalized, Time.deltaTime * rotateSpeed);

        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void CameraRotation(){
        if (!LeftClickInput() || (LeftClickInput() && RightClickInput())) {
            camYRotation += lookInput.x * rotationPower;
            camXRotation += lookInput.y * rotationPower;
            camXRotation = Mathf.Clamp(camXRotation, minXCamClamp, maxXCamClamp);

            Quaternion rotation = Quaternion.Euler(camXRotation, camYRotation, 0);

            followTarget.rotation = rotation;
        }
    }

    private void MouseLookInput(){
        lookInput = playerInputs.CameraMovement.Look.ReadValue<Vector2>();
    }

    private bool LeftClickInput(){
        return playerInputs.CameraMovement.MouseLeftClick.ReadValue<float>() > 0;
    }
    
    private bool RightClickInput(){
        return playerInputs.CameraMovement.MouseRightClick.ReadValue<float>() > 0;
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
        if (moveDirection == Vector3.zero) {
            CameraRotation();
        }
    }
}