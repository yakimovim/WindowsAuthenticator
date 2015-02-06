using System.Configuration;

namespace WindowsAuthenticator.Models.Configuration
{
    public class AuthenticationItemsSection : ConfigurationSection
    {
        [ConfigurationProperty("items")]
        public AuthenticationItemsCollection Items
        {
            get { return (AuthenticationItemsCollection) base["items"]; }
        }
    }
}