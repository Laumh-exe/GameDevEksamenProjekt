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
            playerMovement.SetTiltSpeedModifier(0.5f);
        }
        else {
            playerMovement.SetTiltSpeedModifier(1);
            Quaternion targetRotation = player.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
        }
    }

    private void Tilt(){
        float min = uiTiltControlsScript.GetCircleTiltRadius() * -1;
        float max = uiTiltControlsScript.GetCircleTiltRadius();
        float tiltAngleX = MapValue(yDistance, min,max, -75, 75);
        float tiltAngleZ = MapValue(xDistance, min,max, -75, 75);

        Quaternion targetRotation = Quaternion.Euler(tiltAngleX, 0, tiltAngleZ);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);

    }

    private float MapValue(float value, float oldMin, float oldMax, float newMin, float newMax){
        return (newMax - newMin) * (value - oldMin) / (oldMax - oldMin) + newMin;
    }
    
    private void OnDestroy(){
        playerInputs.Disable();
    }
}