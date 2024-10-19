using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour{
    [Header("CameraMovement")] [SerializeField]
    private Transform followTarget;
    private int minXCamClamp = -40;
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

    [Header("CameraZone")] [SerializeField]
    private CameraZoneSwitcher cameraZone;

    private bool grounded;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerInputs playerInputs;
    private Vector2 lookInput;
    private float camYRotation;
    private float camXRotation;
    private float clickInput;
    private float cameraFollowSpeed = 5f;
    private float rotateSpeed = 8f;
    private float tiltSpeedModifer = 1;
    bool isMoving = false;
    private bool resetCameraY;
    private bool resetCameraX;
    private float timeSinceLastCameraMovement = 0f;
    private float cameraResetDelay = 2f;
    private bool alternativeMove = false;
    private bool blockMove = false;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        playerInputs.CameraMovement.Enable();
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
                
        CameraZoneSwitcher.OnCamChange += OnCamChange;
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
        if (!blockMove) {
            if (alternativeMove) {
                AlternativeMove();
            }
            else {
                Move();
            }
        }
    }

    private void Jump(){
        if (grounded) {
            rb.AddForce(playerModel.transform.up * jumpHeight, ForceMode.Impulse);

            SoundManager.Instance.PlayJumpSound();
        }
    }

    private void Move(){
        float speed = 0;
        horizontalInput = GetMovementVectorNormalized().x;
        verticalInput = GetMovementVectorNormalized().y;
        moveDirection = transform.forward * verticalInput;

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        float targetRotation = 0;

        if (inputDirection != Vector3.zero) {
            isMoving = true;
            speed = moveSpeed;
            targetRotation = Quaternion.LookRotation(inputDirection).eulerAngles.y + mainCamera.transform.rotation.eulerAngles.y;
            Quaternion targetRotationQuaternion = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationQuaternion, Time.deltaTime * rotateSpeed * tiltSpeedModifer);
        }
        else {
            isMoving = false;
        }

        Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        rb.AddForce(targetDirection * (speed * tiltSpeedModifer) * 10f, ForceMode.Force);
    }
    
    private void AlternativeMove(){
        horizontalInput = GetMovementVectorNormalized().x;
        verticalInput = GetMovementVectorNormalized().y;
        float speed = 0;
        
        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        float targetRotation = 0;
        
        if (inputDirection != Vector3.zero) {
            speed = moveSpeed;
            isMoving = true;
            targetRotation = Quaternion.LookRotation(inputDirection).eulerAngles.y;
            Quaternion targetRotationQuaternion = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationQuaternion, Time.deltaTime * rotateSpeed * tiltSpeedModifer);
        }
        else {
            speed = 0;
            isMoving = false;
        }
        
        rb.AddForce(inputDirection * (speed * tiltSpeedModifer) * 10f, ForceMode.Force);
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void CameraRotation(){
        bool cameraMoved = false;

        if (!LeftClickInput()) {
            camYRotation += lookInput.x * rotationPower;
            camXRotation += lookInput.y * rotationPower;
            camXRotation = Mathf.Clamp(camXRotation, minXCamClamp, maxXCamClamp);
        }
        if (lookInput.magnitude > 0.01f) {
            cameraMoved = true;
            timeSinceLastCameraMovement = 0f;
            resetCameraX = false;
            resetCameraY = false;
        }
        if (!cameraMoved) {
            timeSinceLastCameraMovement += Time.deltaTime;

            if (timeSinceLastCameraMovement >= cameraResetDelay) {
                resetCameraX = true;
                resetCameraY = true;
            }
        }
        if (isMoving) {
            resetCameraX = true;
        }
        else if (LeftClickInput()) {
            resetCameraY = true;
            resetCameraX = true;
        }
        if (resetCameraX) {
            camXRotation = Mathf.Lerp(camXRotation, 0, Time.deltaTime * cameraFollowSpeed * .5f);
        }
        if (resetCameraY) {
            camYRotation = Mathf.LerpAngle(camYRotation, transform.rotation.eulerAngles.y, Time.deltaTime * cameraFollowSpeed);
        }

        Quaternion rotation = Quaternion.Euler(camXRotation, camYRotation, 0);

        followTarget.rotation = rotation;
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
    
    private void OnCamChange(bool obj){
        alternativeMove = obj;

        blockMove = true;
        StartCoroutine(UnblockMove());
        
        resetCameraX = true;
        resetCameraY = true;
    }

    private IEnumerator UnblockMove(){
        yield return new WaitForSeconds(.5f);
        blockMove = false;
    }

    private void OnDestroy(){
        playerInputs.Disable();
        CameraZoneSwitcher.OnCamChange -= OnCamChange;
    }

    private void LateUpdate(){
        CameraRotation();
    }

    public void SetTiltSpeedModifier(float modifier){
        tiltSpeedModifer = modifier;
    }
}