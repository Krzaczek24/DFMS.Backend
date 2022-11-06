using DFMS.Database.Dto.Users;

namespace DFMS.WebApi.DataContracts.Logon
{
    public class LogonOutput
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
