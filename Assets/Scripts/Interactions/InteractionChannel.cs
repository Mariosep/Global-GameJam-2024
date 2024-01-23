using System;
using UnityEngine;

public static class InteractionChannel
{
    public static Action<Movable> onImageGrabbed;
    public static Action<Movable, Vector2> onImageMoved;
    public static Action<Movable> onImageReleased;
}