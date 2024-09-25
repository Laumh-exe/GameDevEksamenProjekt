using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour{
    [Header("Movement")] [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float jumpHeight;

    [Header("Ground Check")] [SerializeField]
    private float playerHeight;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float tiltSpeed = 50f;

    private bool grounded;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Vector3 rotateDirection;
    private Rigidbody rb;
    private PlayerInputs playerInputs;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
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
        rotateDirection = transform.right * horizontalInput;


        const float rotateSpeed = 3f;
        transform.forward = Vector3.Slerp(transform.forward, rotateDirection.normalized, Time.deltaTime * rotateSpeed);


        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    /*private void Tilt(){
        Vector2 mousePosition = playerInputs.Player.Mouse.ReadValue<Vector2>();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
            Vector3 targetPosition = hitInfo.point;
            float distance = Vector3.Distance(this.transform.position, targetPosition);

            // Calculate the tilt amount based on the distance
            float maxTiltDistance = 10f; // Set this to the distance at which the tilt is 90 degrees
            float tiltAmount = Mathf.Clamp((distance / maxTiltDistance) * 90f, 0f, 90f);
            //float speedModifier = Math.Clamp(tiltAmount / baseMovementSpeed, 0, baseMovementSpeed);
            //player.SetSpeed(baseMovementSpeed-speedModifier);

            Vector3 direction = (targetPosition - transform.position).normalized;

            Quaternion tiltRotation = Quaternion.Euler(tiltAmount * direction.z, 0, -tiltAmount * direction.x);
            transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, Time.deltaTime * 1000);
        }
    }*/

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
}