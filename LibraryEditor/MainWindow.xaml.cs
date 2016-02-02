using System.Collections.Generic;
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
            var collisions =
                vm
                .LoadedLibrary
                .WouldCollide(lib.AllComponents)
                .Select(c => new MergeCollisionViewModel(c))
                .ToArray();
            var add =
                lib
                .AllComponents
                .Except(collisions.Select(c => c.NewComponent));
            var mergeVM = new MergeWindowViewModel() { Collisions = collisions };

            if (collisions.Any())
            {
                var mergeWindow = new MergeWindow() { DataContext = mergeVM };
                var res = mergeWindow.ShowDialog();
                if (res.HasValue && res.Value)
                {
                    var overwrite = mergeVM.Collisions.Where(c => c.Overwrite).Select(c => c.NewComponent);
                    vm.AddToCurrentLibrary(add);
                    vm.OverwriteInCurrentLibrary(overwrite);
                }
            }
            else
            {
                vm.AddToCurrentLibrary(add);
            }
        }
    }
}
