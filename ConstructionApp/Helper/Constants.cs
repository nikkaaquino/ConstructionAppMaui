namespace ConstructionApp.Helper
{
    public class Constants
    {
        public static string BASE_API_URL = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5003" : "http://localhost:5003";
    }
}
