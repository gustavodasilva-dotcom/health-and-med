using Common.Shared.Abstractions;

namespace Common.Shared.Security
{
    public interface ITokenProvider
    {
        string Create<T>(T user, string role) where T : UserEntity;
    }
}
