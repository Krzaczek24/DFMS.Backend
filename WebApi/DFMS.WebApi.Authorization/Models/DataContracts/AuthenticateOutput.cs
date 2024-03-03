namespace DFMS.WebApi.Authorization.Models.DataContracts
{
    public class AuthenticateOutput
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
