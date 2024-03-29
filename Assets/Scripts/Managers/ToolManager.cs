using System;
using UnityEngine;

public class MoveTool : Singleton<MoveTool>
{
    public bool canMove;
    
    private RectTransform _rectTransform;
    [SerializeField] private Movable imageSelected;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        
        InteractionChannel.onImageGrabbed += OnImageGrabbed;
        InteractionChannel.onImageReleased += OnImageReleased;

        RoundChannel.onDecorPhaseStarted += EnableMoveTool;
        RoundChannel.onDecorPhaseCompleted += DisableMoveTool;
    }

    private void OnDestroy()
    {
        InteractionChannel.onImageGrabbed -= OnImageGrabbed;
        InteractionChannel.onImageReleased -= OnImageReleased;
    }

    private void EnableMoveTool()
    {
        canMove = true;
    }
    
    private void DisableMoveTool()
    {
        canMove = false;
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
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the screen space position to world space
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            imageSelected.SetPosition(worldPosition);
        }
    }
}