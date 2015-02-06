using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using WindowsAuthenticator.Models.Configuration;
using WindowsAuthenticator.Properties;

namespace WindowsAuthenticator.Views.Support.Validation
{
    public class ExistingTitlesRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;

            return ConfigurationStorage.Section.Items.OfType<AuthenticationItem>().Any(i => i.Title.Equals(text, StringComparison.OrdinalIgnoreCase))
                ? new ValidationResult(false, Resources.TitleIsUsed)
                : new ValidationResult(true, null);
        }
    }
}