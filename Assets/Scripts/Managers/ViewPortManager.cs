using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ViewPortManager : Singleton<ViewPortManager>
{
    public RectTransform viewFrame;
    public RectTransform playerResultContainer;
    public RectTransform npcResultContainer;

    public TextMeshProUGUI usernameText;

    public float maxHorizontalDistance = 10f;
    
    private Bounds viewPortBounds;
    
    private RoundData _roundData;
    private GameObject _baseImage;

    private bool mouseIsInside;
    
    private void Awake()
    {
        var viewPortImage = viewFrame.GetComponent<Image>();
        viewPortBounds = viewPortImage.sprite.bounds;
        
        InteractionChannel.onImageGrabbed += OnImageGrabbed;
        InteractionChannel.onImageReleased += OnImageReleased;
        
        HideUsername();
    }

    private void OnDisable()
    {
        InteractionChannel.onImageGrabbed -= OnImageGrabbed;
        InteractionChannel.onImageReleased -= OnImageReleased;
    }

    private void OnImageGrabbed(Movable imageGrabbed)
    {
        imageGrabbed.transform.SetParent(AccessoriesManager.Instance.shelf.transform);
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
            imageReleased.transform.SetParent(AccessoriesManager.Instance.shelf.transform);
        }
    }

    bool ImageIsInside(Vector3 imagePosition)
    {
        var horizontalDistance =  Math.Abs(imagePosition.x - playerResultContainer.position.x);
        return horizontalDistance < maxHorizontalDistance;
    }
    
    public void InstantiateBaseImage(GameObject baseImagePrefab)
    {
        _baseImage = Instantiate(baseImagePrefab, playerResultContainer);
    }

    public void HidePlayerResult()
    {
        playerResultContainer.gameObject.SetActive(false);
    }
    
    public void DestroyPlayerResult()
    {
        foreach (Transform child in playerResultContainer.gameObject.transform)
        {
            if(child.name != "BG")
                Destroy(child.gameObject);
        }
    }
    
    public void ShowPlayerResult()
    {
        playerResultContainer.gameObject.SetActive(true);
    }

    public void ShowNPCResult(GameObject imageResult)
    {
        Instantiate(imageResult, npcResultContainer);
    }
    
    public void HideNPCResult()
    {
        HideUsername();
        if(npcResultContainer.childCount > 0)
            Destroy(npcResultContainer.GetChild(0).gameObject); 
    }

    public void ShowUsername(string username, Color color)
    {
        usernameText.enabled = true;
        usernameText.text = username;
        usernameText.color = color;
    }
    
    public void HideUsername()
    {
        usernameText.enabled = false;
    }
    
    public void DeleteBaseImage()
    {
        if(_baseImage != null)
            Object.Destroy(_baseImage);
    }
    
    private void OnRoundCompleted(RoundController roundControllerCompleted)
    {
        
    }
}