using AQM.Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogData", fileName = "DialogData")]
public class DialogData : ScriptableObject
{
    public ConversationTree waitToStartDialog;
    public ConversationTree preDecorPhaseDialog;
    public ConversationTree decorPhaseDialog;
    public ConversationTree postDecorPhaseDialog;
    public ConversationTree preRatePhaseDialog;
    public ConversationTree ratePhaseDialog;
    public ConversationTree postRatePhaseDialog;
}