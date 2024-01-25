using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<RoundData> rounds;

    public int roundIndex = 0;
    public RoundController roundController;
    
    private void Start()
    {
        roundIndex = 0;
        StartNextRound();
        
        roundController.onRoundCompleted += OnRoundCompleted;
    }

    private void StartNextRound()
    {
        roundController.Begin(rounds[roundIndex], roundIndex + 1);
        roundIndex++;
        
        Debug.Log($"Round {roundController.roundNumber} started");
    }

    private void OnRoundCompleted()
    {
        Debug.Log($"Round {roundController.roundNumber} completed");

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