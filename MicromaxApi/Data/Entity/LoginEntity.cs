using System.ComponentModel.DataAnnotations.Schema;

namespace MicromaxApi.Data.Entity
{
    [Table("tblUsers")]
    public class LoginEntity
    {
        public string UserId { get; set; }
        public string UsrPassword { get; set; }
        public string ContactId { get; set; }
        public string DefaultSystem { get; set; }
        public string UserLevel { get; set; }
        public string RecordLog { get; set; }
        public string UserTypeId { get; set; }
        public string EmpValidation { get; set; }
        public string StartingAccess { get; set; }
        public string LockUpDays { get; set; }
        public string ScheduleAssign { get; set; }
    }
}
