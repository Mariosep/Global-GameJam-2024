using UnityEngine;
using UnityEngine.EventSystems;

public class Movable : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private RectTransform _rectTransform;

    [SerializeField] private bool isSelected = false;

    private Vector3 offsetWithMouse;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector3 newPosition)
    {
        _rectTransform.position = newPosition - offsetWithMouse;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
        
        CalculateOffsetWithMouse(eventData.position);
        InteractionChannel.onImageGrabbed?.Invoke(this);   
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        /*if(isSelected)
        {
            Debug.Log(eventData.position);
            InteractionChannel.onImageMoved?.Invoke(this, eventData.position);
        }*/
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
        InteractionChannel.onImageReleased?.Invoke(this);
    }

    private void CalculateOffsetWithMouse(Vector3 mousePosition)
    {
        offsetWithMouse = mousePosition - _rectTransform.position;
    }
}