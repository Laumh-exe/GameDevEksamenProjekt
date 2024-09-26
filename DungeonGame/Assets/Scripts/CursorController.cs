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
        Vector2 cursorHotspot = new Vector2(cursorType.width/10, cursorType.height/10);
        Cursor.SetCursor(cursorType, cursorHotspot, CursorMode.Auto);
    }
    
    /*private Vector2 ScreenToNormalized(Vector3 screenPos)
    {
        // Get the screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert to a range of (-1, 1)
        float normalizedX = (screenPos.x / screenWidth) * 2 - 1;
        float normalizedY = (screenPos.y / screenHeight) * 2 - 1;

        // Invert the Y axis to match UI coordinate system (top-left is (-1,1))
        normalizedY = -normalizedY;

        return new Vector2(normalizedX, normalizedY);
    }*/
}
