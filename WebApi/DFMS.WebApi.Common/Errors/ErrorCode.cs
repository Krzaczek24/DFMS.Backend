using DFMS.Shared.Converters;
using System.ComponentModel;

namespace DFMS.WebApi.Common.Errors
{
    [EnumToString(NameAlterMode.ToUpperSnake)]
    public enum ErrorCode
    {
        [Description("An unknown error occurred")]
        Unknown,
        [Description("At least one of request fields is invalid")]
        InvalidRequestFieldValue,
        [Description("Selected username is already used")]
        UsernameAlreadyTaken,
        [Description("Resource has been not found")]
        ResourceNotFound,
        [Description("Already exists resource with that name")]
        NonUniqueName,
        [Description("Resource is used by some other resource")]
        ResourceInUse,
        [Description("Already exists such relation")]
        NonUniqueRelation,
        [Description("Failed to authorize")]
        Unauthorized,
        [Description("Insufficient permissions")]
        Forbidden,
        [Description("Token expired")]
        TokenExpired,
        [Description("Invalid token")]
        TokenInvalid,
        [Description("Token already exists")]
        TokenExists
    }
}
