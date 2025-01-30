namespace Common.Shared.Options;

public sealed class KillSwitchOptions
{
    public required int ActivationThreshold { get; init; }

    public required double TripThreshold { get; init; }

    public required int RestartMinutesTimeout { get; init; }
}
