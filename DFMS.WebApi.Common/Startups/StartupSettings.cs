namespace DFMS.WebApi.Common.Startups
{
    public interface IStartupSettings
    {
        public ISwaggerSetings? Swagger { get; }
    }

    public class StartupSettings : IStartupSettings
    {
        public ISwaggerSetings? Swagger { get; set; }
    }
}
