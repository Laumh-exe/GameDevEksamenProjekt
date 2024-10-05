using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour{

    public Vector2 move;
    public Vector2 look;
    
    private PlayerInputs playerInputs;
    void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Player.Enable();
        playerInputs.CameraMovement.Enable();
    }
    
    private void OnLook(InputValue value){
        look = value.Get<Vector2>();
    }

    private void OnMove(InputValue value){
        move =  value.Get<Vector2>();
    }

    void Update()
    {
        
    }
}
