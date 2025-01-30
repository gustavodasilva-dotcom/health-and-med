namespace Common.Shared.Options;

public sealed class MessageBrokerOptions
{
    public const string Position = "MessageBroker";

    public required string Host { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }

    public required int NumberOfRetries { get; init; }

    public required KillSwitchOptions KillSwitch { get; init; }
}
