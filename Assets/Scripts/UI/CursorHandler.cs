using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D grabCursor;
    
    private void OnEnable()
    {
        InteractionChannel.onImageGrabbed += OnImageGrabbed;
        InteractionChannel.onImageReleased += OnImageReleased;
    }

    private void OnDisable()
    {
        InteractionChannel.onImageGrabbed -= OnImageGrabbed;
        InteractionChannel.onImageReleased -= OnImageReleased;
    }

    private void OnImageReleased(Movable imageReleased)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnImageGrabbed(Movable imageGrabbed)
    {
        Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);
    }
}
