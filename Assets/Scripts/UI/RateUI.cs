using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RateUI : Singleton<RateUI>
{
    public GameObject contentContainer;
    
    public Button pukeButton;
    public Button fineButton;
    public Button funnyButton;

    public bool playerHasRated;
    public RatingType playerRating;

    private Animator _animator;
    private RoundController _roundController;
    private RoundData _roundData => _roundController.roundData;

    private void Awake()
    {
        contentContainer = transform.GetChild(0).gameObject;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        contentContainer.SetActive(false);
        
        pukeButton.onClick.AddListener(() => OnRateButtonClicked(RatingType.Puke));
        fineButton.onClick.AddListener(() => OnRateButtonClicked(RatingType.Fine));
        funnyButton.onClick.AddListener(() => OnRateButtonClicked(RatingType.Funny));
    }

    public void ShowRatePanel(RoundController roundController)
    {
        _roundController = roundController;

        playerHasRated = false;
        contentContainer.SetActive(true);
        _animator.SetBool("ShowPanel", true);
        
        RoundChannel.onRatePhaseStarted?.Invoke();
    }

    public void HideRatePanel()
    {
        contentContainer.SetActive(true);
        
        _animator.SetBool("ShowPanel", false);
        
        RoundChannel.onRatePhaseStarted?.Invoke();
    }
    
    private void OnRateButtonClicked(RatingType rating)
    {
        playerHasRated = true;
        playerRating = rating;
    }
}

public enum RatingType
{
    None,
    Puke,
    Fine,
    Funny
}
