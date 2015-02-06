using System.Configuration;

namespace WindowsAuthenticator.Models.Configuration
{
    public static class ConfigurationStorage
    {
        public static readonly System.Configuration.Configuration Configuration;
        public static readonly AuthenticationItemsSection Section;

        static ConfigurationStorage()
        {
            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);

            Section = (AuthenticationItemsSection)Configuration.GetSection("authenticationItems");
        }

    }
}