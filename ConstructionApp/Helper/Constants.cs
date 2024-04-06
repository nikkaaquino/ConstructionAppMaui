namespace ConstructionApp.Helper
{
    public class Constants
    {
        public static string BASE_API_URL = DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.9:5003/api" : "http://192.168.1.9:5003/api";
    }
}
