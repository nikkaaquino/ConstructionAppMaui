namespace ConstructionApp.Model.Response
{
    public class PhotoDetailsResponse
    {
        public string AccessToken { get; private set; }

        public PhotoDetailsResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
