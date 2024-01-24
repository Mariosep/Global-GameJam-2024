using System;

public static class RoundChannel
{
    public static Action<Round> onRoundStarted;
    public static Action<Round> onRoundCompleted;
}