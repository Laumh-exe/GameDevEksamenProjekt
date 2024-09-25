using System;
using UnityEngine;

public class CursorController : MonoBehaviour{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Texture2D cursorClicked;
    [SerializeField] private Texture2D cursorPull;
    [SerializeField] private GameObject canvas;

    private void Update(){
        // Get mouse position in screen coordinates (pixels)
        Vector3 mousePos = Input.mousePosition;

        // Convert mouse position to normalized coordinates (-1 to 1)
        Vector2 normalizedMousePos = ScreenToNormalized(mousePos);
        if(Input.)
        
        if (Input.GetMouseButton(0)) {
            Cursor.visible = true;
            ChangeCursor(cursorPull);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else {
            Cursor.visible = false;
        }
    }

    private void ChangeCursor(Texture2D cursorType){
        Vector2 cursorHotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, cursorHotspot, CursorMode.Auto);
    }
    
    private Vector2 ScreenToNormalized(Vector3 screenPos)
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
    }
}
