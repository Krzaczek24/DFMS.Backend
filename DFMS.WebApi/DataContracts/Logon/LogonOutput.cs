using DFMS.Database.Dto.Users;
using DFMS.WebApi.Authorization;

namespace DFMS.WebApi.DataContracts.Logon
{
    public class LogonOutput
    {
        public User User { get; set; }
        public TokenData TokenData { get; set; }
    }
}
