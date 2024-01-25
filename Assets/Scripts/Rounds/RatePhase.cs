using System.Collections;
using UnityEngine;

public class RatePhase : Singleton<RatePhase>, IPhase
{
    private RoundController _currentRoundController;
    
    private RoundData roundData => _currentRoundController.roundData;
    
    public void Begin(RoundController roundController)
    {
        _currentRoundController = roundController;
        
        StartCoroutine(PrePhase());
    }

    public IEnumerator PrePhase()
    {
        yield return new WaitForSeconds(3f);
        ViewPortManager.Instance.HidePlayerResult();

        StartCoroutine(StartPhase());
    }

    public IEnumerator StartPhase()
    {
        yield return new WaitForSeconds(3f);

        foreach (GameObject imageResult in roundData.npcResults)
        {
            ViewPortManager.Instance.HideNPCResult(imageResult);
            ViewPortManager.Instance.ShowNPCResult(imageResult);
            
            RateUI.Instance.StartRate(_currentRoundController);

            yield return new WaitForSeconds(5f);
            
            Debug.Log("Show rate result");
            
            yield return new WaitForSeconds(5f);
        }
        
        ViewPortManager.Instance.ShowPlayerResult();
        
        yield return new WaitForSeconds(5f);
        
        Debug.Log("Show rate result");
            
        yield return new WaitForSeconds(5f);

        StartCoroutine(PostPhase());
    }
    
    public IEnumerator PostPhase()
    {
        yield return new WaitForSeconds(3f);
        
        RoundChannel.onRatePhaseCompleted?.Invoke();
    }
}