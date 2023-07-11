using System.ComponentModel;

namespace DFMS.WebApi.Core.Errors
{
    public enum ErrorCode
    {
        [Description("An unknown error occurred")]
        UNKNOWN,
        [Description("At least one of request fields is invalid")]
        INVALID_REQUEST_FIELD_VALUE,
        [Description("Selected username is already used")]
        USERNAME_ALREADY_TAKEN,
        [Description("Resource has been not found")]
        RESOURCE_NOT_FOUND,
        [Description("Already exists resource with that name")]
        NON_UNIQUE_NAME,
        [Description("Resource is used by some other resource")]
        RESOURCE_IN_USE,
        [Description("Already exists such relation")]
        NON_UNIQUE_RELATION,
        [Description("Failed to authorize")]
        UNAUTHORIZED,
        [Description("Insufficient permissions")]
        FORBIDDEN,
        [Description("Token expired")]
        TOKEN_EXPIRED,
        [Description("Invalid token")]
        TOKEN_INVALID,
        [Description("Token already exists")]
        TOKEN_EXISTS
    }
}
