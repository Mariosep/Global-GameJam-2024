using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform rectTransform;

    private Vector3 offsetWithMouse;
    [SerializeField] private bool isSelected;
    
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
        if(MoveTool.Instance.canMove)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position,
                eventData.pressEventCamera, out Vector3 worldPoint);

            CalculateOffsetWithMouse(worldPoint);
            isSelected = true;
            InteractionChannel.onImageGrabbed?.Invoke(this);
        }   
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(isSelected)
        {
            InteractionChannel.onImageReleased?.Invoke(this);
            isSelected = false;
        }
    }

    private void CalculateOffsetWithMouse(Vector3 mousePosition)
    {
        offsetWithMouse = mousePosition - rectTransform.position;
    }
}