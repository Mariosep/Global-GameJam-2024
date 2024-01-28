using System;

public static class RoundChannel
{
    public static Action<RoundController> onRoundStarted;
    public static Action<RoundController> onRoundCompleted;
    public static Action onPreDecorPhase;
    public static Action onDecorPhaseStarted;
    public static Action onPostDecorPhase;
    public static Action onDecorPhaseCompleted;
    public static Action onPreRatePhase;
    public static Action onRatePhaseStarted;
    public static Action onPostRatePhase;
    public static Action onRatePhaseCompleted;
    
    public static Action onBaseImageShowed;
}

public static class ServerChannel
{
    public static Action onServerJoined;
}