namespace MoE.ECE.Domain.Abstractions
{
    public interface ILoggedOnUser
    {
        bool IsAuthenticated { get; }
        string UserName { get; }    
    }
}