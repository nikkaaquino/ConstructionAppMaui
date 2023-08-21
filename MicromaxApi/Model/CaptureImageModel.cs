namespace MicromaxApi.Model
{
    public class CaptureImageModel
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string CreatedBy { get; set; }
    }
}
