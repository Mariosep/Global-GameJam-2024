using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<RoundData> rounds;

    public GameObject serverUI;
    public GameObject appUI;
    
    public int roundIndex = 0;
    public RoundController roundController;

    public bool startGameOnPlay;

    public int waitToStart;
    
    private void Awake()
    {
        ServerChannel.onServerJoined += StartGame;
    }

    private void OnDestroy()
    {
        ServerChannel.onServerJoined -= StartGame;
    }

    private void Start()
    {
        if (startGameOnPlay)
            StartCoroutine(WaitingToStart());
    }

    private IEnumerator WaitingToStart()
    {
        RoundChannel.onWaitToStart?.Invoke();
        
        WaitingToStartUI.Instance.ShowWaitingPanel();
        
        yield return new WaitForSeconds(waitToStart);
        
        WaitingToStartUI.Instance.HideWaitingPanel();
        
        yield return new WaitForSeconds(3);
        
        StartGame();
    }
    
    private void StartGame()
    {
        serverUI.SetActive(false);
        appUI.SetActive(true);
        
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