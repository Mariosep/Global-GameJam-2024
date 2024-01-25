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
    }

    public IEnumerator StartPhase()
    {
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator PostPhase()
    {
        yield return new WaitForSeconds(3f);
    }
}