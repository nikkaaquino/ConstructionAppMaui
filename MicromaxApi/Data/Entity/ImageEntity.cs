using System.ComponentModel.DataAnnotations.Schema;

namespace MicromaxApi.Data.Entity
{
    [Table("tblImages")]
    public class ImageEntity
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageData { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Location { get; set; }
        public string User { get; set; }
        public byte[] ImageView { get; set; }
        public string ImageType { get; set; }
    }
}


