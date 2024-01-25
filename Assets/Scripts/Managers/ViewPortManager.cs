using UnityEngine;
using UnityEngine.UI;

public class ViewPortManager : Singleton<ViewPortManager>
{
    public RectTransform playerResultContainer;
    public RectTransform npcResultContainer;
    
    public Transform accessoriesContainer;
    
    private Bounds viewPortBounds;
    
    private RoundData _roundData;
    private GameObject _baseImage;

    private bool mouseIsInside;
    
    private void Awake()
    {
        var viewPortImage = GetComponent<Image>();
        viewPortBounds = viewPortImage.sprite.bounds;
        
        InteractionChannel.onImageReleased += OnImageReleased;
    }

    private void OnDisable()
    {
        InteractionChannel.onImageReleased -= OnImageReleased;
    }

    private void OnImageReleased(Movable imageReleased)
    {
        Bounds imageReleasedBounds = imageReleased.GetComponent<Image>().sprite.bounds;
        Vector3 imagePosition = imageReleased.rectTransform.position;

        if (ImageIsInside(imagePosition))
        {
            Debug.Log("Image is inside the bounds!");
            imageReleased.transform.SetParent(playerResultContainer);
        }
        else
        {
            imageReleased.transform.SetParent(accessoriesContainer);
        }
    }

    public void InstantiateBaseImage(GameObject baseImagePrefab)
    {
        _baseImage = Instantiate(baseImagePrefab, playerResultContainer);
    }

    public void HidePlayerResult()
    {
        playerResultContainer.gameObject.SetActive(false);
    }
    
    public void ShowPlayerResult()
    {
        playerResultContainer.gameObject.SetActive(true);
    }

    public void ShowNPCResult(GameObject imageResult)
    {
         Instantiate(imageResult, npcResultContainer);
    }
    
    public void HideNPCResult(GameObject imageResult)
    {
        if(npcResultContainer.childCount > 0)
            Destroy(npcResultContainer.GetChild(0).gameObject); 
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
        return playerResultContainer.rect.Contains(imagePosition);
    }
}