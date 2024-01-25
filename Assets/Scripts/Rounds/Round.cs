using System;
using System.Collections;
using AQM.Tools;
using UnityEngine;

public class Round
{
    public Action onRoundCompleted;
    public int roundNumber;

    public RoundData RoundData;
    
    private int _timeLeft;
    
    public int TimeLeft => _timeLeft;
    
    public enum RoundState
    {
        Pending,
        InProgress,
        Completed
    }

    public Round(RoundData roundData, int roundNumber)
    {
        this.RoundData = roundData;
        this.roundNumber = roundNumber;
    }

    public IEnumerator Start()
    {
        RoundChannel.onRoundStarted?.Invoke(this);
        if (RoundData.conversation)
        {
            DDEvents.onStartConversation?.Invoke(RoundData.conversation);
        }
        
        float startTime = Time.time;
        float timePassed = 0;
        
        _timeLeft = Mathf.CeilToInt(RoundData.roundTime);
        
        while (_timeLeft > 0)
        {
            timePassed = Time.time - startTime;
            _timeLeft = Mathf.CeilToInt(RoundData.roundTime - timePassed);
            
            yield return new WaitForEndOfFrame();
        }
        
        onRoundCompleted?.Invoke();
        RoundChannel.onRoundCompleted?.Invoke(this);
    }
}