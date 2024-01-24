using UnityEngine;

public class ViewPortManager : MonoBehaviour
{
    public Transform viewportContainer;
    
    private RoundData _roundData;
    private GameObject _baseImage;
    
    private void Awake()
    {
        RoundChannel.onRoundStarted += OnRoundStarted;
        RoundChannel.onRoundCompleted += OnRoundCompleted;
    }

    private void OnDisable()
    {
        RoundChannel.onRoundStarted -= OnRoundStarted;
        RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }
    
    private void OnRoundStarted(Round roundStarted)
    {
        _roundData = roundStarted.RoundData;
        
        if(_baseImage != null)
            Object.Destroy(_baseImage);
        
        _baseImage = Instantiate(_roundData.baseImage, transform);
    }
    
    private void OnRoundCompleted(Round roundCompleted)
    {
        
    }
}