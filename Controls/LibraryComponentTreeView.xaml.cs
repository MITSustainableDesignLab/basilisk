using System.Collections.Generic;
using System.Linq;
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

        private ICollection<object> SelectedItems
        {
            get { return (ICollection<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static DependencyProperty ComponentsProperty =
            DependencyProperty.Register(
                nameof(Components),
                typeof(ComponentCategoryCollection),
                typeof(LibraryComponentTreeView),
                new FrameworkPropertyMetadata(OnComponentsChanged));

        public static DependencyProperty SelectedComponentProperty =
            DependencyProperty.Register(
                nameof(SelectedComponent),
                typeof(LibraryComponent),
                typeof(LibraryComponentTreeView));

        private static DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                nameof(SelectedItems),
                typeof(ICollection<object>),
                typeof(LibraryComponentTreeView),
                new FrameworkPropertyMetadata(new List<object>(), OnSelectedItemsChanged));

        private static void OnComponentsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var tree = (LibraryComponentTreeView)o;
            tree.SelectedItems = new List<object>();
        }

        private static void OnSelectedItemsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var selection = (ICollection<object>)e.NewValue;
            var treeView = (LibraryComponentTreeView)o;
            treeView.SelectedComponent =
                selection.Count == 1 ? (LibraryComponent)selection.First() :
                selection.Count > 1 ? new LibraryComponentSet(selection.Cast<LibraryComponent>()) :
                null;
        }
    }
}
