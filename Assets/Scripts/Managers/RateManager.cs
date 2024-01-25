using System;
using UnityEngine;
using UnityEngine.UI;

public class RateManager : Singleton<RateManager>
{
    public enum RateType
    {
        None,
        Puke,
        Fine,
        Funny
    }

    public GameObject ratePanel;
    
    public Button pukeButton;
    public Button fineButton;
    public Button funnyButton;

    public bool playerHasVoted;
    public RateType playerRating;

    private RoundController _currentRoundController;

    private Animator _animator;
    private static readonly int ShowPanel = Animator.StringToHash("ShowPanel");

    private void Awake()
    {
         _animator = ratePanel.GetComponent<Animator>();
    }

    private void Start()
    {
        ratePanel.SetActive(false);
        
        pukeButton.onClick.AddListener(() => OnRateButtonClicked(RateType.Puke));
        fineButton.onClick.AddListener(() => OnRateButtonClicked(RateType.Fine));
        funnyButton.onClick.AddListener(() => OnRateButtonClicked(RateType.Funny));
    }

    public void StartRate(RoundController roundController)
    {
        _currentRoundController = roundController;
        
        ratePanel.SetActive(true);
        
        _animator.SetBool(ShowPanel, true);
        
        RoundChannel.onRatePhaseStarted?.Invoke();
    }

    private void OnRateButtonClicked(RateType rate)
    {
        playerHasVoted = true;
        playerRating = rate;
        //CompleteRate();
        
        _animator.SetBool(ShowPanel, false);
    }

    private void CompleteRate()
    {
        ratePanel.SetActive(false);
        RoundChannel.onRatePhaseCompleted?.Invoke();
        _currentRoundController.Complete();
    }
}
