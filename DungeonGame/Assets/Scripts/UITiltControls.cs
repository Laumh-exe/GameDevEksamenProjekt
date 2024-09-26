using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UITiltControls : MonoBehaviour{
    [SerializeField] private GameObject staticTiltControlImage;
    [SerializeField] private GameObject mouseHoldTiltControlImage;
    [SerializeField] private Canvas canvas;
    
    
    private GameObject staticImage;
    private GameObject movingImage;
    private Vector2 clickPosition;
    private Vector2 dragPosition;
    private float yDistance;
    

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button pressed");
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
        Vector2 mousePosition = Input.mousePosition;

        if (staticImage == null) {
           staticImage = Instantiate(staticTiltControlImage, canvas.transform);
        }

        RectTransform rectTransform = staticImage.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out Vector2 localPoint);
        clickPosition = localPoint;
        rectTransform.anchoredPosition = clickPosition;
    }

    private void ImageFollowMouse(){
        Vector2 mousePosition = Input.mousePosition;

        if (movingImage == null)
        {
            movingImage = Instantiate(mouseHoldTiltControlImage, canvas.transform);
        }

        RectTransform rectTransform = movingImage.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out Vector2 localPoint);
        Vector2 dragPosition = Vector2.Lerp(clickPosition, localPoint, 0.5f);
        rectTransform.anchoredPosition = dragPosition;
        
        yDistance = dragPosition.y - clickPosition.y;

        // Log the distance for debugging
        Debug.Log("Y Distance: " + yDistance);
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

}
