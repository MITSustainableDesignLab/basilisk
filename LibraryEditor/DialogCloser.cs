using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Basilisk.LibraryEditor
{
    // http://blog.excastle.com/2010/07/25/mvvm-and-dialogresult-with-no-code-behind/
    //<Window ...
    //    whatever:DialogCloser.DialogResult="{Binding DialogResult}">
    // Create a DialogResult property (as a bool?) on your view model. Assigning anything to it will
    // cause the associated window to be closed.
    internal static class DialogCloser
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.DialogResult = e.NewValue as bool?;
        }
        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }
    }
}
