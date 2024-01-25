using System.Collections;
using UnityEngine;

public interface IPhase
{
    public void Begin(RoundController roundController);
    public IEnumerator PrePhase();
    public IEnumerator StartPhase();
    public IEnumerator PostPhase();
}


public class DecorPhase : Singleton<DecorPhase>, IPhase
{
    public float preDecorTime;

    private RoundController _currentRoundController;

    private int _timeLeft;
    public int TimeLeft => _timeLeft;
    
    private RoundData roundData => _currentRoundController.roundData;
    
    public void Begin(RoundController roundController)
    {
        _currentRoundController = roundController;
        
        StartCoroutine(PrePhase());
    }
    
    public IEnumerator PrePhase()
    {
        _currentRoundController.state = RoundState.PreDecor;
        
        ViewPortManager.Instance.InstantiateBaseImage(_currentRoundController.roundData.baseImage);

        yield return new WaitForSeconds(preDecorTime);
        
        RoundChannel.onBaseImageShowed?.Invoke();
        
        yield return new WaitForSeconds(3);

        StartCoroutine(StartPhase());
    }
    
    public IEnumerator StartPhase()
    {
        _currentRoundController.state = RoundState.Decor;
        
        RoundChannel.onDecorPhaseStarted?.Invoke();
        
        float startTime = Time.time;
        float timePassed = 0;
        
        _timeLeft = Mathf.CeilToInt(roundData.roundTime);
        
        while (_timeLeft > 0)
        {
            timePassed = Time.time - startTime;
            _timeLeft = Mathf.CeilToInt(roundData.roundTime - timePassed);
            
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(PostPhase());
    }
    
    public IEnumerator PostPhase()
    {
        _currentRoundController.state = RoundState.PostDecor;
        
        yield return null;
        RoundChannel.onDecorPhaseCompleted?.Invoke();
    }
}