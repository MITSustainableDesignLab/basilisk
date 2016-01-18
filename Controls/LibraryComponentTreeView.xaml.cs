﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for LibraryComponentTreeView.xaml
    /// </summary>
    public partial class LibraryComponentTreeView : UserControl
    {
        public const string IdentifiedLibraryDragDropFormat = "IdentifiedLibrary";
        public readonly Guid Identifer = Guid.NewGuid();

        public LibraryComponentTreeView()
        {
            InitializeComponent();
            var dragger = new MouseEventHandler(componentsTree_MouseMove);
            AddHandler(MouseMoveEvent, dragger, handledEventsToo: true);
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

        private void componentsTree_MouseMove(object sender, MouseEventArgs e)
        {
            var tree = sender as LibraryComponentTreeView;
            if (tree != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var selected = tree.SelectedItems.Cast<LibraryComponent>();
                var all = selected.Concat(selected.SelectMany(c => c.AllReferencedComponents));
                var json = Library.CreateSublibrary(all).ToJson();
                var identifiedJson = new SourcedLibraryJson()
                {
                    SourceId = Identifer,
                    Json = json
                };
                var dataObject = new DataObject();
                dataObject.SetData(DataFormats.StringFormat, json);
                dataObject.SetData(IdentifiedLibraryDragDropFormat, identifiedJson.ToString());
                DragDrop.DoDragDrop(tree, dataObject, DragDropEffects.Copy);
            }
        }

        public class SourcedLibraryJson
        {
            public Guid SourceId { get; set; }
            public string Json { get; set; }

            public static SourcedLibraryJson FromString(string serialized)
            {
                var id = Guid.Parse(serialized.Split(';')[0]);
                var json = serialized.Substring(id.ToString().Length + 1);
                return new SourcedLibraryJson() { SourceId = id, Json = json };
            }

            public override string ToString() => $"{SourceId.ToString()};{Json}";
        }
    }
}
