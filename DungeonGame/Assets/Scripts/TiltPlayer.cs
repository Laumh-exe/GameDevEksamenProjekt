using System;
using Unity.VisualScripting;
using UnityEngine;

public class TiltPlayer : MonoBehaviour{
    [SerializeField] private float tiltSpeed = 50f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject uiTiltControls;

    private PlayerInputs playerInputs;
    private Camera mainCamera;
    private float baseMovementSpeed;
    private PlayerMovement playerMovement;
    private UITiltControls uiTiltControlsScript;
    private float yDistance = 0;
    private float xDistance = 0;

    private void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        mainCamera = Camera.main;
    }

    private void Start(){
        playerMovement = player.GetComponent<PlayerMovement>();
        baseMovementSpeed = playerMovement.GetSpeed();
        uiTiltControlsScript = uiTiltControls.GetComponent<UITiltControls>();
    }

    void Update(){
        yDistance = uiTiltControlsScript.GetYDistance();
        xDistance = uiTiltControlsScript.GetXDistance();
        
        if (Input.GetMouseButton(0)) {
            Tilt();
        }
        else {
            Quaternion targetRotation = player.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
        }
    }

    private void Tilt(){
        /*Vector2 mousePosition = playerInputs.Player.Mouse.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) {
            Vector3 targetPosition = hitInfo.point;
            float distance = Vector3.Distance(this.transform.position, targetPosition);
            Vector3 direction = targetPosition - player.transform.position;
            direction.y = 0;

            float tiltAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            tiltAngle = Mathf.Clamp(tiltAngle, -75, 75);*/
        float tiltAngleX = MapValue(yDistance, -200,200, -75, 75);
        float tiltAngleZ = MapValue(xDistance, -200,200, -75, 75);

        Quaternion targetRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);

        //How to imrpove this
        //When clicking mouse - you will pull small circle from whereever you clicked. The more you pull circle up, the more player will tilt up. The more you pull circle down, the more player will tilt down. This way tilting amount is base on where mouse is dragged to and direction is based on direction on monitor

        //}
    }

    private float MapValue(float value, float oldMin, float oldMax, float newMin, float newMax){
        return (newMax - newMin) * (value - oldMin) / (oldMax - oldMin) + newMin;
    }
}