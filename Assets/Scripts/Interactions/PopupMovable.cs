using UnityEngine;
using UnityEngine.EventSystems;

public class PopupMovable : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform rectTransform;
    
    private Vector3 offsetWithMouse;
    [SerializeField] private bool isSelected;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        if (isSelected)
        {
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen space position to world space
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            SetPosition(worldPosition);
        }
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
        isSelected = true;
        InteractionChannel.onPopupGrabbed?.Invoke();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(isSelected)
        {
            InteractionChannel.onPopupReleased?.Invoke();
            isSelected = false;   
        }
    }
    
    private void CalculateOffsetWithMouse(Vector3 mousePosition)
    {
        offsetWithMouse = mousePosition - rectTransform.position;
    }
}
