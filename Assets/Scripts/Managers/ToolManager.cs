using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTool : MonoBehaviour
{
    [SerializeField] private Movable imageSelected;
    
    private void Awake()
    {
        InteractionChannel.onImageGrabbed += OnImageGrabbed;
        InteractionChannel.onImageReleased += OnImageReleased;
    }

    private void OnImageReleased(Movable obj)
    {
        imageSelected = null;
    }

    private void OnImageGrabbed(Movable img)
    {
        imageSelected = img;
    }

    private void Update()
    {
        if (imageSelected)
        {
            Vector3 mousePosition = Input.mousePosition;
            imageSelected.SetPosition(mousePosition);
        }
    }
}