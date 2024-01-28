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
    public float waitAfterShelfShowed;
    public float waitAfterBaseImageShowed;
    public float waitAfterDecorTimeEnded;

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
        RoundChannel.onPreDecorPhase?.Invoke();
        
        AccessoriesManager.Instance.ShowShelf(roundData.shelfPrefab);
        
        yield return new WaitForSeconds(waitAfterShelfShowed);
        
        _currentRoundController.state = RoundState.PreDecor;
        
        ViewPortManager.Instance.InstantiateBaseImage(_currentRoundController.roundData.baseImagePrefab);

        yield return new WaitForSeconds(waitAfterBaseImageShowed);
        
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
        RoundChannel.onPostDecorPhase?.Invoke();
        
        _currentRoundController.state = RoundState.PostDecor;

        yield return new WaitForSeconds(3f);
        
        AccessoriesManager.Instance.HideShelf();
        
        yield return new WaitForSeconds(waitAfterDecorTimeEnded);
        
        RoundChannel.onDecorPhaseCompleted?.Invoke();
    }
}