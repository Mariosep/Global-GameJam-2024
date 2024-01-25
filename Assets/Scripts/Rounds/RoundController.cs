using System;
using AQM.Tools;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public Action onRoundCompleted;
    public int roundNumber;

    public RoundData roundData;
    
    public RoundState state;

    public DecorPhase decorPhase;
    public RatePhase ratePhase;

    private void Awake()
    {
        RoundChannel.onDecorPhaseCompleted += StartRatePhase;
        RoundChannel.onRatePhaseCompleted += Complete;
    }

    private void OnDestroy()
    {
        RoundChannel.onDecorPhaseCompleted -= StartRatePhase;
    }

    public void Begin(RoundData roundData, int roundNumber)
    {
        this.roundData = roundData;
        this.roundNumber = roundNumber;
        
        state = RoundState.Pending;
        
        RoundChannel.onRoundStarted?.Invoke(this);
        
        if (roundData.conversation)
        {
            DDEvents.onStartConversation?.Invoke(roundData.conversation);
        }
        
        StartDecorPhase();
    }

    private void StartDecorPhase()
    {
        decorPhase.Begin(this);
    }
    
    private void StartRatePhase()
    {
        ratePhase.Begin(this);
        RateManager.Instance.StartRate(this);
    }

    public void Complete()
    {
        state = RoundState.Completed;
        
        onRoundCompleted?.Invoke();
        RoundChannel.onRoundCompleted?.Invoke(this);
    }
}

public enum RoundState
{
    Pending,
    PreDecor,
    Decor,
    PostDecor,
    Vote,
    Completed
}