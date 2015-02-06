using System.Configuration;

namespace WindowsAuthenticator.Models.Configuration
{
    [ConfigurationCollection(typeof(AuthenticationItem), AddItemName = "item")]
    public class AuthenticationItemsCollection :  ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthenticationItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthenticationItem)(element)).Title;
        }

        public AuthenticationItem this[int idx]
        {
            get { return (AuthenticationItem)BaseGet(idx); }
        }

        public void Add(AuthenticationItem item)
        {
            BaseAdd(item);
        }

        public void Remove(string title)
        {
            BaseRemove(title);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}