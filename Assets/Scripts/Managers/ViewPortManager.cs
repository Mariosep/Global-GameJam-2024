using UnityEngine;

public class ViewPortManager : Singleton<ViewPortManager>
{
    public Transform viewportContainer;
    
    private RoundData _roundData;
    private GameObject _baseImage;
    
    private void Awake()
    {
        //RoundChannel.onRoundStarted += OnRoundStarted;
        //RoundChannel.onRoundCompleted += OnRoundCompleted;
    }

    private void OnDisable()
    {
        //RoundChannel.onRoundStarted -= OnRoundStarted;
        //RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }

    public void InstantiateBaseImage(GameObject baseImagePrefab)
    {
        _baseImage = Instantiate(baseImagePrefab, transform);
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