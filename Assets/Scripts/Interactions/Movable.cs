using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform rectTransform;

    private Vector3 offsetWithMouse;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector3 newPosition)
    {
        rectTransform.position = newPosition - offsetWithMouse;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
            eventData.pressEventCamera, out Vector3 worldPoint);
        
        CalculateOffsetWithMouse(worldPoint);
        InteractionChannel.onImageGrabbed?.Invoke(this);   
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        InteractionChannel.onImageReleased?.Invoke(this);
    }

    private void CalculateOffsetWithMouse(Vector3 mousePosition)
    {
        offsetWithMouse = mousePosition - rectTransform.position;
    }
}