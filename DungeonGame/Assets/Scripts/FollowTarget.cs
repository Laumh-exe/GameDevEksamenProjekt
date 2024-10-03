using UnityEngine;
using UnityEngine.UIElements;

public class FollowTarget : MonoBehaviour{
    [SerializeField] private float rotationPower = 3f;

    private PlayerInputs playerInputs;

    void Awake(){
        playerInputs = new PlayerInputs();
        playerInputs.CameraMovement.Enable();
    }

    void Update(){
        Vector2 cameraMovement = playerInputs.CameraMovement.Move.ReadValue<Vector2>();
        VerticalRotation(cameraMovement.x);
        HorizontalRotation(cameraMovement.y);
    }

    private void ClampVerticalRotation(){
        var angles = transform.localEulerAngles;
        angles.z = 0;

        var angle = transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340) {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40) {
            angles.x = 40;
        }

        transform.localEulerAngles = angles;
    }

    private void VerticalRotation(float y){
        if (!Input.GetMouseButton(0)) {
            transform.rotation *= Quaternion.AngleAxis(y * rotationPower, Vector3.up);
            ClampVerticalRotation();
        }
        
    }

    private void HorizontalRotation(float x){
        if (!Input.GetMouseButton(0)) {
            transform.rotation *= Quaternion.AngleAxis(x * rotationPower, Vector3.right);
        }
    }
}