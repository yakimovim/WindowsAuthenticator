using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WindowsAuthenticator.Views.Support
{
    internal static class WindowHelper
    {
        public static List<object> GetValidationErrors(this Window window)
        {
            var errors = new List<object>();

            GetErrors(errors, window);

            return errors;
        }

        private static void GetErrors(List<object> errors, DependencyObject obj)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                var element = child as DependencyObject;
                if (element == null)
                { continue; }

                if (System.Windows.Controls.Validation.GetHasError(element))
                {
                    foreach (var error in System.Windows.Controls.Validation.GetErrors(element))
                    {
                        errors.Add(error.ErrorContent);
                    }
                }

                GetErrors(errors, element);
            }
        }

        public static void ValidateTextBoxes(this Window window)
        {
            RecursivelyValidateTextBoxes(window);
        }

        private static void RecursivelyValidateTextBoxes(DependencyObject obj)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                var element = child as DependencyObject;
                if (element == null)
                { continue; }

                var textBox = element as TextBox;
                if (textBox != null)
                {
                    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                RecursivelyValidateTextBoxes(element);
            }
        }
    }
}
