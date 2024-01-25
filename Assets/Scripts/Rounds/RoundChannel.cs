using System;

public static class RoundChannel
{
    public static Action<RoundController> onRoundStarted;
    public static Action<RoundController> onRoundCompleted;
    public static Action onDecorPhaseStarted;
    public static Action onDecorPhaseCompleted;
    public static Action onRatePhaseStarted;
    public static Action onRatePhaseCompleted;
    
    public static Action onBaseImageShowed;
}