using System;
using Unity.VisualScripting;
using UnityEngine;

public class TiltPlayer : MonoBehaviour
{
    [SerializeField] private float tiltSpeed = 50f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject tiltAxis;
    
    private PlayerInputs playerInputs;
    private Camera mainCamera;
    private float baseMovementSpeed;
    private PlayerMovement playerMovement;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        mainCamera = Camera.main;
    }

    private void Start(){
        playerMovement = player.GetComponent<PlayerMovement>();
        baseMovementSpeed = playerMovement.GetSpeed();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Tilt();
        }
        else {
            Quaternion targetRotation = player.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
        }
    }
    
    private void Tilt(){
        Vector2 mousePosition = playerInputs.Player.Mouse.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
            Vector3 targetPosition = hitInfo.point;
            float distance = Vector3.Distance(this.transform.position, targetPosition);
            Vector3 direction = targetPosition - player.transform.position;
            direction.y = 0;
            
            float tiltAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            
            tiltAngle = Mathf.Clamp(tiltAngle, -75, 75);
            
            Quaternion targetRotation = Quaternion.Euler(tiltAngle, 0, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);

            
        }
    }
}
