using System.Windows;

namespace WindowsAuthenticator.Views.Support.Validation
{
    public static class ValidationHelper
    {
        public static bool GetValidateBeforeClose(DependencyObject obj)
        { return (bool)obj.GetValue(ValidateBeforeCloseProperty); }

        public static void SetValidateBeforeClose(DependencyObject obj, bool value)
        { obj.SetValue(ValidateBeforeCloseProperty, value); }

        public static readonly DependencyProperty ValidateBeforeCloseProperty = DependencyProperty.RegisterAttached("ValidateBeforeClose",
            typeof(bool),
            typeof(ValidationHelper),
            new UIPropertyMetadata(false));
    }
}
