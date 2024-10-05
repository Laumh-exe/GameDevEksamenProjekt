using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UITiltControls : MonoBehaviour{
    [SerializeField] private GameObject staticTiltControlImage;
    [SerializeField] private GameObject mouseHoldTiltControlImage;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float circleTiltRadius = 30;
    
    private GameObject staticImage;
    private GameObject movingImage;
    private Vector2 clickPosition;
    private Vector2 dragPosition;
    private Vector2 initialMousePosition;
    private Vector2 spawnPosition = new Vector2(Screen.width / 2, Screen.height / 8);

    private float yDistance = 0;
    private float xDistance = 0;

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            SpawnImageAtClick();
        }

        if (Input.GetMouseButton(0)) {
            ImageFollowMouse();
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            DestroyImage();
        }
    }

    private void SpawnImageAtClick(){
        initialMousePosition = Input.mousePosition;

        if (staticImage == null) {
           staticImage = Instantiate(staticTiltControlImage, canvas.transform);
        }

        RectTransform rectTransform = staticImage.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, spawnPosition, canvas.worldCamera, out Vector2 localPoint);
        clickPosition = localPoint;
        rectTransform.anchoredPosition = clickPosition;
    }

    private void ImageFollowMouse(){
        Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 distance = currentMousePosition - initialMousePosition;
        
        /*if (Vector2.Distance(initialMousePosition, currentMousePosition) > circleTiltRadius) {
            //InitialMousePosition should more 1 in direction of currentMousePosition so they are always circleTiltRadius apart
            Vector2 direction = (currentMousePosition - initialMousePosition).normalized;

            // Update the initialMousePosition to be circleTiltRadius away from currentMousePosition
            initialMousePosition = currentMousePosition - direction * circleTiltRadius;
        }*/
        
        Vector2 mousePosition = spawnPosition + distance;

        if (movingImage == null)
        {
            movingImage = Instantiate(mouseHoldTiltControlImage, canvas.transform);
        }

        RectTransform rectTransform = movingImage.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out Vector2 localPoint);
        
        Vector2 dragPosition = localPoint - clickPosition;
        
        float distanceFromCenter = dragPosition.magnitude;
        
        if (distanceFromCenter > circleTiltRadius) {
            dragPosition = dragPosition.normalized * circleTiltRadius; // Normalize and multiply by max radius
        }
        
        rectTransform.anchoredPosition = clickPosition + dragPosition;

        xDistance = dragPosition.x;
        yDistance = dragPosition.y;
    }

    private void DestroyImage()
    {
        if (staticImage != null)
        {
            Destroy(staticImage);
        }
        if (movingImage != null)
        {
            Destroy(movingImage);
        }
    }
    
    public float GetYDistance(){
        return yDistance;
    }
    
    public float GetXDistance(){
        return xDistance;
    }
    
    public float GetCircleTiltRadius(){
        return circleTiltRadius;
    }
}
