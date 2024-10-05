using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CursorController : MonoBehaviour{
    [SerializeField] private Texture2D customCursor;

    private void Start(){
        EnableCursor();
        SetCursor(customCursor);
    }

    private void SetCursor(Texture2D cursorType){
        if (cursorType != null) {
            Vector2 cursorHotspot = new Vector2(cursorType.width/10, cursorType.height/10);
            Cursor.SetCursor(cursorType, cursorHotspot, CursorMode.Auto);
        }
    }
    
    public void DisableCursor(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public void EnableCursor(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
