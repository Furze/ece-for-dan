namespace MoE.ECE.Domain.Infrastructure.Abstractions
{
    public interface ILoggedOnUser
    {
        bool IsAuthenticated { get; }
        string UserName { get; }
    }
}