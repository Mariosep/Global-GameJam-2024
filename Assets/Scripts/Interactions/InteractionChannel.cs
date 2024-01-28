using System;

public static class InteractionChannel
{
    public static Action<Movable> onImageGrabbed;
    public static Action<Movable> onImageReleased;
    
    public static Action onPopupGrabbed;
    public static Action onPopupReleased;
}