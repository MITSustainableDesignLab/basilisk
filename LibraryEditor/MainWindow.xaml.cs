using System.Linq;
using System.Windows;

using Basilisk.Controls;
using Basilisk.Controls.InterfaceModels;
using Basilisk.LibraryEditor.ViewModels;

namespace Basilisk.LibraryEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var window = (MainWindowViewModel)DataContext;
            e.Cancel = !window.ConfirmWindowClosing();
        }

        private void componentsTree_Drop(object sender, DragEventArgs e)
        {
            var tree = sender as LibraryComponentTreeView;
            if (tree != null)
            {
                try
                {
                    var format = LibraryComponentTreeView.IdentifiedLibraryDragDropFormat;
                    if (e.Data.GetDataPresent(LibraryComponentTreeView.IdentifiedLibraryDragDropFormat))
                    {
                        var serialized = (string)e.Data.GetData(format, autoConvert: false);
                        var identifed = LibraryComponentTreeView.SourcedLibraryJson.FromString(serialized);
                        if (identifed.SourceId == tree.Identifer) { return; }
                        var json = identifed.Json;
                        var lib = Library.Create(Core.Library.FromJson(json));
                        TryImport(lib);
                    }
                    else if (e.Data.GetDataPresent(DataFormats.StringFormat))
                    {
                        var json = (string)e.Data.GetData(DataFormats.StringFormat);
                        var lib = Library.Create(Core.Library.FromJson(json));
                        TryImport(lib);
                    }
                }
                catch
                {
                    MessageBox.Show(
                        "Failed to parse dropped data (was it a library file?)",
                        "Import failure",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void TryImport(Library lib)
        {
            var vm = (MainWindowViewModel)this.DataContext;
            var collisions = vm.LoadedLibrary.WouldCollide(lib).ToArray();
            if (collisions.Any())
            {
                MessageBox.Show(
                    "The components could not be imported because there would be name collisions (name merging is not yet supported).",
                    "Name collisions",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                vm.ImportIntoCurrentLibrary(lib);
            }
        }
    }
}
