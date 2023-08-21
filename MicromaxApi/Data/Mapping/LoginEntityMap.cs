using Dapper.FluentMap.Mapping;
using MicromaxApi.Data.Entity;

namespace MicromaxApi.Data.Mapping
{
    public class LoginEntityMap : EntityMap<LoginEntity>
    {
        public LoginEntityMap()
        {

            this.Map(x => x.UserId).ToColumn("UserID");
            this.Map(x => x.UsrPassword).ToColumn("UsrPassword");
            this.Map(x => x.ContactId).ToColumn("ContactID");
            this.Map(x => x.DefaultSystem).ToColumn("DefaultSystem");
            this.Map(x => x.UserLevel).ToColumn("UserLevel");
            this.Map(x => x.RecordLog).ToColumn("RecordLog");
            this.Map(x => x.UserTypeId).ToColumn("UserTypeID");
            this.Map(x => x.EmpValidation).ToColumn("EmployeeValidation");
            this.Map(x => x.StartingAccess).ToColumn("EndingAccess");
            this.Map(x => x.LockUpDays).ToColumn("LockUpDays");
            this.Map(x => x.ScheduleAssign).ToColumn("ScheduleAssign");
        }
    }
}
