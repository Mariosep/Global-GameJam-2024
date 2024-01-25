using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewPortManager : Singleton<ViewPortManager>, IDragHandler
{
    public RectTransform viewportContainer;
    public Transform accessoriesContainer;

    private Bounds viewPortBounds;
    
    private RoundData _roundData;
    private GameObject _baseImage;

    private bool mouseIsInside;
    
    private void Awake()
    {
        //RoundChannel.onRoundStarted += OnRoundStarted;
        //RoundChannel.onRoundCompleted += OnRoundCompleted;
        var viewPortImage = GetComponent<Image>();
        viewPortBounds = viewPortImage.sprite.bounds;
        
        RoundChannel.onDecorPhaseCompleted += HidePlayerDecorResult;
        
        InteractionChannel.onImageReleased += OnImageReleased;
    }

    private void OnDestroy()
    {
        RoundChannel.onDecorPhaseCompleted -= HidePlayerDecorResult;
        InteractionChannel.onImageReleased -= OnImageReleased;
        //RoundChannel.onRoundStarted -= OnRoundStarted;
        //RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }

    private void OnImageReleased(Movable imageReleased)
    {
        Bounds imageReleasedBounds = imageReleased.GetComponent<Image>().sprite.bounds;
        Vector3 imagePosition = imageReleased.rectTransform.position;

        if (ImageIsInside(imagePosition))
        {
            Debug.Log("Image is inside the bounds!");
            imageReleased.transform.SetParent(viewportContainer);
        }
        else
        {
            imageReleased.transform.SetParent(accessoriesContainer);
        }
    }

    public void InstantiateBaseImage(GameObject baseImagePrefab)
    {
        _baseImage = Instantiate(baseImagePrefab, viewportContainer.transform);
    }

    private void HidePlayerDecorResult()
    {
        viewportContainer.gameObject.SetActive(false);
    }
    
    public void DeleteBaseImage()
    {
        if(_baseImage != null)
            Object.Destroy(_baseImage);
    }
    
    private void OnRoundCompleted(RoundController roundControllerCompleted)
    {
        
    }
    
    bool ImageIsInside(Vector3 imagePosition)
    {
        return viewportContainer.rect.Contains(imagePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }
}