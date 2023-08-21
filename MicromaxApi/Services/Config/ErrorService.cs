namespace MicromaxApi.Services.Config
{
    public class ErrorService : IErrorService
    {
        private Dictionary<string, object> _validation;

        protected ErrorService()
        {
            _validation = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Validation => _validation;

        public bool IsValid => _validation.Count == 0;
    }
}
