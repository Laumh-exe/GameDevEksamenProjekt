using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour{
    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float groundDrag;

    [Header("Ground Check")] [SerializeField]
    private float playerHeight;

    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;


    [SerializeField] private float tiltSpeed = 50f;
    private Camera mainCamera;

    private PlayerInputs playerInputs;
    private Vector2 mousePosition;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        mainCamera = Camera.main;
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update(){
        mousePosition = playerInputs.Player.Mouse.ReadValue<Vector2>();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);
        SpeedControl();
        
        if (grounded) {
            rb.linearDamping = groundDrag;
        }
        else {
            rb.linearDamping = 0;
        }

        if (Input.GetMouseButton(0)) {
            Tilt();
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * tiltSpeed);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void FixedUpdate(){
        Move();
    }

    private void Move(){
        //this.transform.position += new Vector3(getMovementVectorNormalized().x, 0, getMovementVectorNormalized().y) * moveSpeed * Time.deltaTime;
        horizontalInput = getMovementVectorNormalized().x;
        verticalInput = getMovementVectorNormalized().y;
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }


    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private Vector2 getMovementVectorNormalized(){
        Vector2 inputVector = playerInputs.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    private void Tilt(){
        //tilt amount is controlled by distance from player. max tilt is 90 degrees and can be set to a certain distance
        //tilt direction is controlled by the direction of the mouse from the player(alternativly we just rotate the player towards the mouse)
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
            Vector3 targetPosition = hitInfo.point;
            float distance = Vector3.Distance(this.transform.position, targetPosition);

            // Calculate the tilt amount based on the distance
            float maxTiltDistance = 10f; // Set this to the distance at which the tilt is 90 degrees
            float tiltAmount = Mathf.Clamp((distance / maxTiltDistance) * 90f, 0f, 90f);

            // Determine the tilt direction
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Apply the tilt to the player
            Quaternion tiltRotation = Quaternion.Euler(tiltAmount * direction.z, 0, -tiltAmount * direction.x);
            transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, Time.deltaTime * 1000);
        }
    }
}