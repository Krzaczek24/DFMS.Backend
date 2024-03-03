namespace DFMS.WebApi.Common.Startups
{
    public interface ISwaggerSetings
    {
        public string Group { get; }
        public string Name { get; }
    }

    public class SwaggerSettings : ISwaggerSetings
    {
        public string Group { get; } = string.Empty;
        public string Name { get; } = string.Empty;

        public SwaggerSettings() { }

        public SwaggerSettings(string group, string name)
        {
            Group = group;
            Name = name;
        }
    }
}
