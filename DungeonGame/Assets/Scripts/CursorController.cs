using System;
using UnityEngine;

public class CursorController : MonoBehaviour{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Texture2D cursorClicked;
    [SerializeField] private Texture2D cursorPull;

    private void Start(){
        SetCursor(cursorPull);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void SetCursor(Texture2D cursorType){
        if (cursorType != null) {
            Vector2 cursorHotspot = new Vector2(cursorType.width/10, cursorType.height/10);
            Cursor.SetCursor(cursorType, cursorHotspot, CursorMode.Auto);
        }
    }
}
