namespace MicromaxApi.Services.Config
{
    public interface IErrorService
    {
        Dictionary<string, object> Validation { get; }
        bool IsValid { get; }
    }
}
