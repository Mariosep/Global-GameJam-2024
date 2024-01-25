using UnityEngine;
using UnityEngine.UI;

public class RateUI : Singleton<RateUI>
{
    public enum RateType
    {
        None,
        Puke,
        Fine,
        Funny
    }

    public GameObject contentContainer;
    
    public Button pukeButton;
    public Button fineButton;
    public Button funnyButton;

    public bool playerHasVoted;
    public RateType playerRating;

    private RoundController _currentRoundController;

    private void Awake()
    {
        contentContainer = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        contentContainer.SetActive(false);
        
        pukeButton.onClick.AddListener(() => OnRateButtonClicked(RateType.Puke));
        fineButton.onClick.AddListener(() => OnRateButtonClicked(RateType.Fine));
        funnyButton.onClick.AddListener(() => OnRateButtonClicked(RateType.Funny));
    }

    public void StartRate(RoundController roundController)
    {
        _currentRoundController = roundController;
        
        contentContainer.SetActive(true);
        
        //_animator.SetBool(ShowPanel, true);
        
        RoundChannel.onRatePhaseStarted?.Invoke();
    }

    private void OnRateButtonClicked(RateType rate)
    {
        playerHasVoted = true;
        playerRating = rate;
        //CompleteRate();
        
        //_animator.SetBool(ShowPanel, false);
    }

    private void CompleteRate()
    {
        contentContainer.SetActive(false);
        RoundChannel.onRatePhaseCompleted?.Invoke();
        _currentRoundController.Complete();
    }
}
