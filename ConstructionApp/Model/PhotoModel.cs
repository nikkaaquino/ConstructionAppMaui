
namespace ConstructionApp.Model
{
    public class PhotoModel
    {
        public int ImageId { get; set; }

        public string ImageName { get; set; }

        public byte[] ImageData { get; set; }

        public string CreatedBy { get; set; }

        public PhotoModel Clone() => MemberwiseClone() as PhotoModel;

        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(ImageName))
            {
                return (false, $"{nameof(ImageName)} is required.");
            }
            return (true, null);
        }
    }
}
