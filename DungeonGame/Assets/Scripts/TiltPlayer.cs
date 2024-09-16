using System;
using Unity.VisualScripting;
using UnityEngine;

public class TiltPlayer : MonoBehaviour
{
    [SerializeField] private float tiltSpeed = 50f;
    [SerializeField] private PlayerMovement player;
    
    private PlayerInputs playerInputs;
    private Camera mainCamera;
    private float baseMovementSpeed;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        mainCamera = Camera.main;
    }

    private void Start(){
        baseMovementSpeed = player.GetSpeed();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Tilt();
        }
        else {
            //Check if tilting back

            Quaternion targetRotation = player.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
            //transform.position = new Vector3(transform.position.x, 1, transform.position.z);
           // player.SetSpeed(baseMovementSpeed);
           
        }
    }
    
    private void Tilt(){
        Vector2 mousePosition = playerInputs.Player.Mouse.ReadValue<Vector2>();
        
        //tilt amount is controlled by distance from player. max tilt is 90 degrees and can be set to a certain distance
        //tilt direction is controlled by the direction of the mouse from the player(alternativly we just rotate the player towards the mouse)
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
            Vector3 targetPosition = hitInfo.point;
            float distance = Vector3.Distance(this.transform.position, targetPosition);

            // Calculate the tilt amount based on the distance
            float maxTiltDistance = 10f; // Set this to the distance at which the tilt is 90 degrees
            float tiltAmount = Mathf.Clamp((distance / maxTiltDistance) * 90f, 0f, 90f);
            //float speedModifier = Math.Clamp(tiltAmount / baseMovementSpeed, 0, baseMovementSpeed);
            //player.SetSpeed(baseMovementSpeed-speedModifier);

            // Determine the tilt direction
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Apply the tilt to the player
            Quaternion tiltRotation = Quaternion.Euler(tiltAmount * direction.z, 0, -tiltAmount * direction.x);
            transform.rotation = Quaternion.Slerp(transform.rotation, tiltRotation, Time.deltaTime * 1000);
        }
    }
}
