using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class RatePhase : Singleton<RatePhase>, IPhase
{
    public float waitAfterResultShowed;
    public float waitAfterPlayerHasRated;
    public float waitAfterRatingShowed;

    private RoundController _roundController;
    private RoundData roundData => _roundController.roundData;
    
    public void Begin(RoundController roundController)
    {
        _roundController = roundController;
        
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
        for (var index = 0; index < roundData.npcResults.Count; index++)
        {
            var imageResult = roundData.npcResults[index];
            ViewPortManager.Instance.HideNPCResult();
            ViewPortManager.Instance.ShowNPCResult(imageResult);

            RateUI.Instance.ShowRatePanel(_roundController);

            while (!RateUI.Instance.playerHasRated)
                yield return new WaitForEndOfFrame();

            RateUI.Instance.HideRatePanel();

            yield return new WaitForSeconds(waitAfterPlayerHasRated);

            RatingObtainedUI.Instance.ShowRatingObtainedPanel();
            
            yield return new WaitForSeconds(4f);
            
            RatingObtainedUI.Instance.ShowResult(roundData.npcRatings[index]);
            
            yield return new WaitForSeconds(waitAfterRatingShowed);
            
            RatingObtainedUI.Instance.HideRatingObtainedPanel();
        }

        ViewPortManager.Instance.HideNPCResult();
        ViewPortManager.Instance.ShowPlayerResult();
        
        yield return new WaitForSeconds(waitAfterResultShowed);
        
        yield return new WaitForSeconds(5f);
        
        RatingObtainedUI.Instance.ShowRatingObtainedPanel();
        
        yield return new WaitForSeconds(4f);
        
        RatingObtainedUI.Instance.ShowResult(roundData.playerRating);
        
        yield return new WaitForSeconds(waitAfterRatingShowed);
        
        RatingObtainedUI.Instance.HideRatingObtainedPanel();

        StartCoroutine(PostPhase());
    }
    
    public IEnumerator PostPhase()
    {
        yield return new WaitForSeconds(3f);
        
        RoundChannel.onRatePhaseCompleted?.Invoke();
    }
}