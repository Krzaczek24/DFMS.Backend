using DFMS.Shared.Converters;

namespace DFMS.Shared.Enums
{
    [EnumToString(NameAlterMode.ToUpper)]
    public enum UserRole
    {
        Undefined,
        Blocked,
        Applicant,
        Invitee,
        User,
        Manager,
        Moderator,
        Admin    
    }
}
