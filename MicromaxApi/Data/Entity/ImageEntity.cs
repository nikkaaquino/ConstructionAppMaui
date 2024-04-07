using System.ComponentModel.DataAnnotations.Schema;

namespace MicromaxApi.Data.Entity
{
    [Table("tblImages")]
    public class ImageEntity
    {
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        public string Location { get; set; }
        public string User { get; set; }
        public DateTime? DateCreated { get; set; }

        public string ImagePath { get; set; }
    }
}


