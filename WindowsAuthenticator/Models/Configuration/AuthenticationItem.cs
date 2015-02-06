using System.Configuration;

namespace WindowsAuthenticator.Models.Configuration
{
    public class AuthenticationItem : ConfigurationElement
    {
        [ConfigurationProperty("title", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Title
        {
            get { return (string) base["title"]; }
            set { base["title"] = value; }
        }

        [ConfigurationProperty("secret", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Secret
        {
            get { return (string)base["secret"]; }
            set { base["secret"] = value; }
        }
    }
}