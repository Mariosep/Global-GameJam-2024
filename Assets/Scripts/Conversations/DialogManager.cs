using AQM.Tools;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public bool disableDialogs;
    
    private DialogData _dialogData;
    
    public ConversationTree waitToStartDialog;
    
    private void Awake()
    {
        if(!disableDialogs)
        {
            RoundChannel.onWaitToStart += OnWaitToStart;
            
            RoundChannel.onRoundStarted += OnRoundStarted;

            RoundChannel.onPreDecorPhase += OnPreDecorPhase;
            RoundChannel.onDecorPhaseStarted += OnDecorPhaseStarted;
            RoundChannel.onPostDecorPhase += OnPostDecorPhase;

            RoundChannel.onPreRatePhase += OnPreDecorPhase;
            RoundChannel.onRatePhaseStarted += OnDecorPhaseStarted;
            RoundChannel.onPostRatePhase += OnPostDecorPhase;
        }
    }

    private void OnDestroy()
    {
        RoundChannel.onWaitToStart -= OnWaitToStart;
        RoundChannel.onRoundStarted -= OnRoundStarted;

        RoundChannel.onPreDecorPhase -= OnPreDecorPhase;
        RoundChannel.onDecorPhaseStarted -= OnDecorPhaseStarted;
        RoundChannel.onPostDecorPhase -= OnPostDecorPhase;
        
        RoundChannel.onPreRatePhase -= OnPreDecorPhase;
        RoundChannel.onRatePhaseStarted -= OnDecorPhaseStarted;
        RoundChannel.onPostRatePhase -= OnPostDecorPhase;
    }
    
    private void OnWaitToStart()
    {
        if (waitToStartDialog)
            DDEvents.onStartConversation?.Invoke(waitToStartDialog);
    }
    
    private void OnRoundStarted(RoundController roundController)
    {
        _dialogData = roundController.roundData.dialogData;
    }
    
    private void OnPreDecorPhase()
    {
        if (_dialogData.preDecorPhaseDialog)
            DDEvents.onStartConversation?.Invoke(_dialogData.preDecorPhaseDialog);
    }
    
    private void OnDecorPhaseStarted()
    {
        if (_dialogData.decorPhaseDialog)
            DDEvents.onStartConversation?.Invoke(_dialogData.decorPhaseDialog);
    }
    
    private void OnPostDecorPhase()
    {
        if (_dialogData.postDecorPhaseDialog)
            DDEvents.onStartConversation?.Invoke(_dialogData.postDecorPhaseDialog);
    }

    private void OnPreRatePhase()
    {
        if (_dialogData.preRatePhaseDialog)
            DDEvents.onStartConversation?.Invoke(_dialogData.preRatePhaseDialog);
    }
    
    private void OnRateStarted()
    {
        if (_dialogData.ratePhaseDialog)
            DDEvents.onStartConversation?.Invoke(_dialogData.ratePhaseDialog);
    }
    
    private void OnPostRatePhase()
    {
        if (_dialogData.postRatePhaseDialog)
            DDEvents.onStartConversation?.Invoke(_dialogData.postRatePhaseDialog);
    }
}
