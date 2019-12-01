namespace WebApi.AppUtilities
{
    public interface IAppLogger<T>
    {
        void LogResourceRequest(string caller, string username);
        void LogError(string message, string caller);
    }
}