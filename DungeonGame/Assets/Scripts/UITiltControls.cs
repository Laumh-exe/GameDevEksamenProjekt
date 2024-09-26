using UnityEngine;
using UnityEngine.EventSystems;

public class UITiltControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
    [SerializeField] private GameObject StaticTiltControlImage;
    [SerializeField] private Canvas canvas;
    
    private GameObject staticImage;

    public void OnPointerDown(PointerEventData eventData){
        SpawnImageAtClick();
        Debug.Log("Begin Drag");
    }

    public void OnPointerUp(PointerEventData eventData){
        //DestroyImage();
        Debug.Log("End Drag");
    }

    private void SpawnImageAtClick(){
        DestroyImage();
        Vector2 mousePosition = Input.mousePosition;

        staticImage = Instantiate(StaticTiltControlImage, canvas.transform);

        RectTransform rectTransform = staticImage.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = mousePosition;
    }

    private void DestroyImage()
    {
        // Destroy the spawned image when the pointer is released
        if (staticImage != null)
        {
            Destroy(staticImage);
        }
    }

}
