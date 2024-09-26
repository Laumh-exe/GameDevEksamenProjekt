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
        float tiltAngleX = MapValue(yDistance, -200,200, -75, 75);
        float tiltAngleZ = MapValue(xDistance, -200,200, -75, 75);

        Quaternion targetRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);

    }

    private float MapValue(float value, float oldMin, float oldMax, float newMin, float newMax){
        return (newMax - newMin) * (value - oldMin) / (oldMax - oldMin) + newMin;
    }
}