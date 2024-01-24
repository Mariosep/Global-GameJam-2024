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
        currentRound = new Round(rounds[roundIndex], roundIndex + 1);
        currentRound.onRoundCompleted += OnRoundCompleted;

        Debug.Log($"Round {currentRound.roundNumber} started");

        StartCoroutine(currentRound.Start());

        roundIndex++;
    }

    private void OnRoundCompleted()
    {
        Debug.Log($"Round {currentRound.roundNumber} completed");

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