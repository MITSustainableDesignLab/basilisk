using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for LibraryComponentTreeView.xaml
    /// </summary>
    public partial class LibraryComponentTreeView : UserControl
    {
        public LibraryComponentTreeView()
        {
            InitializeComponent();
        }

        public ComponentCategoryCollection Components
        {
            get { return (ComponentCategoryCollection)GetValue(ComponentsProperty); }
            set { SetValue(ComponentsProperty, value); }
        }

        public LibraryComponent SelectedComponent
        {
            get { return (LibraryComponent)GetValue(SelectedComponentProperty); }
            set { SetValue(SelectedComponentProperty, value); }
        }

        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedComponent = e.NewValue as LibraryComponent;
        }

        public static DependencyProperty ComponentsProperty =
            DependencyProperty.Register(
                nameof(Components),
                typeof(ComponentCategoryCollection),
                typeof(LibraryComponentTreeView));

        public static DependencyProperty SelectedComponentProperty =
            DependencyProperty.Register(
                nameof(SelectedComponent),
                typeof(LibraryComponent),
                typeof(LibraryComponentTreeView));
    }
}
