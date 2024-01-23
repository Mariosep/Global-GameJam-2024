using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<RoundData> rounds;

    public int roundIndex = 0;
    public Round currentRound;

    void Start()
    {
        roundIndex = 0;
        StartNextRound();
    }

    private void StartNextRound()
    {
        currentRound = new Round(rounds[roundIndex]);
        currentRound.onRoundCompleted += OnRoundCompleted;

        Debug.Log($"Round {roundIndex} started");

        StartCoroutine(currentRound.Start());

        roundIndex++;
    }

    private void OnRoundCompleted()
    {
        Debug.Log($"Round {roundIndex} completed");

        if (roundIndex < rounds.Count)
        {
            StartNextRound();
        }
        else
        {
            Debug.Log("The End");
        }
    }
}

public class Round
{
    public Action onRoundCompleted;
    
    public RoundData roundData;

    public enum RoundState
    {
        Pending,
        InProgress,
        Completed
    }

    public Round(RoundData roundData)
    {
        this.roundData = roundData;
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(roundData.roundTime);
        
        onRoundCompleted?.Invoke();
    }
}
