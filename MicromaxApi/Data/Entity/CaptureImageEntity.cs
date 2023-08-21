using System.ComponentModel.DataAnnotations.Schema;

namespace MicromaxApi.Data.Entity
{
    [Table("tblImageUpload")]
    public class CaptureImageEntity
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}
