using System.Collections;
using AQM.Tools;
using UnityEngine;

public class RatePhase : Singleton<RatePhase>, IPhase
{
    public float waitAfterResultShowed;
    public float waitAfterPlayerHasRated;
    public float waitAfterRatingShowed;
    public float waitAfterRatingHided;

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
            Actor npcActor = DialogSystemController.Instance.DialogSystemDatabase.actors[index];
            
            ViewPortManager.Instance.HideNPCResult();
            ViewPortManager.Instance.ShowNPCResult(imageResult);
            ViewPortManager.Instance.ShowUsername(npcActor.fullName, npcActor.bgColor);

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
            
            yield return new WaitForSeconds(waitAfterRatingHided);
        }

        int actorsCount = DialogSystemController.Instance.DialogSystemDatabase.actors.Count;
        Actor playerActor = DialogSystemController.Instance.DialogSystemDatabase.actors[actorsCount-1];
        
        ViewPortManager.Instance.HideNPCResult();
        ViewPortManager.Instance.ShowPlayerResult();
        ViewPortManager.Instance.ShowUsername(playerActor.fullName, playerActor.bgColor);
        
        //yield return new WaitForSeconds(waitAfterResultShowed);
        
        WaitingRateUI.Instance.ShowWaitingRatePanel();
        
        yield return new WaitForSeconds(5f);
        
        WaitingRateUI.Instance.HideRatePanel();
        
        yield return new WaitForSeconds(waitAfterPlayerHasRated);
        
        RatingObtainedUI.Instance.ShowRatingObtainedPanel();
        
        yield return new WaitForSeconds(4f);
        
        RatingObtainedUI.Instance.ShowResult(roundData.playerRating);
        
        yield return new WaitForSeconds(waitAfterRatingShowed);
        
        RatingObtainedUI.Instance.HideRatingObtainedPanel();
        
        yield return new WaitForSeconds(waitAfterRatingHided);

        ViewPortManager.Instance.DestroyPlayerResult();
        
        StartCoroutine(PostPhase());
    }
    
    public IEnumerator PostPhase()
    {
        yield return new WaitForSeconds(3f);
        
        RoundChannel.onRatePhaseCompleted?.Invoke();
    }
}