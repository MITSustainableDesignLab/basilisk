using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Basilisk.Controls
{
    // http://chrigas.blogspot.com/2014/08/wpf-treeview-with-multiple-selection.html
    public static class Multiselect
    {
        private static readonly MouseButtonEventHandler mouseDownHandler = new MouseButtonEventHandler(OnMouseDown);
        private static readonly MouseButtonEventHandler mouseUpHandler = new MouseButtonEventHandler(OnMouseUp);

        public static bool GetAllow(TreeView tree) => (bool)tree.GetValue(AllowProperty);
        public static void SetAllow(TreeView tree, bool val) => tree.SetValue(AllowProperty, val);

        public static readonly DependencyProperty AllowProperty = DependencyProperty.RegisterAttached(
            "Allow",
            typeof(bool),
            typeof(Multiselect),
            new FrameworkPropertyMetadata(OnAllowMultiselectChanged));

        public static bool GetIsItemSelected(TreeViewItem item) => (bool)item.GetValue(IsItemSelectedProperty);
        public static void SetIsItemSelected(TreeViewItem item, bool val) => item.SetValue(IsItemSelectedProperty, val);

        public static readonly DependencyProperty IsItemSelectedProperty = DependencyProperty.RegisterAttached(
            "IsItemSelected",
            typeof(bool),
            typeof(Multiselect),
            new FrameworkPropertyMetadata(OnIsItemSelectedChanged));

        public static ICollection<object> GetSelectedItems(TreeView tree) => (ICollection<object>)tree.GetValue(SelectedItemsProperty);
        public static void SetSelectedItems(TreeView tree, ICollection<object> val) => tree.SetValue(SelectedItemsProperty, val);

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
            "SelectedItems",
            typeof(ICollection<object>),
            typeof(Multiselect),
            new FrameworkPropertyMetadata(new List<object>()));

        private static TreeViewItem GetStartItem(TreeView tree) => (TreeViewItem)tree.GetValue(StartItemProperty);
        private static void SetStartItem(TreeView tree, TreeViewItem val) => tree.SetValue(StartItemProperty, val);

        private static readonly DependencyProperty StartItemProperty = DependencyProperty.RegisterAttached(
            "StartItem",
            typeof(TreeViewItem),
            typeof(Multiselect));

        private static TreeViewItem GetJustChangedSelection(TreeView tree) => (TreeViewItem)tree.GetValue(JustChangedSelectionProperty);
        private static void SetJustChangedSelection(TreeView tree, TreeViewItem val) => tree.SetValue(JustChangedSelectionProperty, val);

        private static readonly DependencyProperty JustChangedSelectionProperty = DependencyProperty.RegisterAttached(
            "JustChangedSelection",
            typeof(TreeViewItem),
            typeof(Multiselect));

        private static void AddSequenceToSelection(this TreeView tree, TreeViewItem until)
        {
            var start = GetStartItem(tree);
            if (start != null)
            {
                if (start == until)
                {
                    tree.SelectSingleItem(start);
                    return;
                }
                var all = GetSelfAndDescendants(tree);
                DeselectSelfAndDescendants(tree);
                var isBetween = false;
                foreach (var item in all)
                {
                    if (item == until || item == start)
                    {
                        isBetween = !isBetween;
                        SetIsItemSelected(item, true);
                        continue;
                    }
                    if (isBetween) { SetIsItemSelected(item, true); }
                }
            }
        }

        private static void DeselectSelfAndDescendants(ItemsControl control)
        {
            if (control == null) { return; }
            var item = control as TreeViewItem;
            if (item != null) { SetIsItemSelected(item, false); }
            for (var i = 0; i < control.Items.Count; ++i)
            {
                var child = control.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                DeselectSelfAndDescendants(child);
            }
        }

        private static TParent FindAncestor<TParent>(DependencyObject o)
            where TParent : class
        {
            if (o == null) { return null; }
            return o as TParent ?? FindAncestor<TParent>(VisualTreeHelper.GetParent(o));
        }

        private static IEnumerable<TreeViewItem> GetSelfAndDescendants(ItemsControl control)
        {
            if (control == null) { return Enumerable.Empty<TreeViewItem>(); }
            var item = control as TreeViewItem;
            var these = item == null
                ? Enumerable.Empty<TreeViewItem>()
                : Enumerable.Repeat(item, 1);
            for (var i = 0; i < control.Items.Count; ++i)
            {
                var child = control.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                these = these.Concat(GetSelfAndDescendants(child));
            }
            return these;
        }

        private static void OnAllowMultiselectChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var tree = o as TreeView;
            if (tree != null)
            {
                if (e.NewValue is bool)
                {
                    if ((bool)e.NewValue)
                    {
                        tree.AddHandler(TreeViewItem.PreviewMouseLeftButtonDownEvent, mouseDownHandler);
                        tree.AddHandler(TreeViewItem.PreviewMouseLeftButtonUpEvent, mouseUpHandler);
                    }
                    else
                    {
                        tree.RemoveHandler(TreeViewItem.PreviewMouseLeftButtonDownEvent, mouseDownHandler);
                        tree.RemoveHandler(TreeViewItem.PreviewMouseLeftButtonUpEvent, mouseUpHandler);
                    }
                }
            }
        }

        private static void OnIsItemSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var item = o as TreeViewItem;
            var tree = FindAncestor<TreeView>(item);
            if (item != null && tree != null)
            {
                var selection = GetSelectedItems(tree);
                if (selection != null)
                {
                    if (GetIsItemSelected(item)) { selection.Add(item.Header); }
                    else { selection.Remove(item.Header); }
                }
            }
        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = FindAncestor<TreeViewItem>(e.OriginalSource as DependencyObject);
            var tree = sender as TreeView;
            if (item != null && tree != null)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (!GetIsItemSelected(item))
                    {
                        tree.ToggleRandomSelection(item);
                        SetJustChangedSelection(tree, item);
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    tree.AddSequenceToSelection(item);
                }
                else
                {
                    if (!GetIsItemSelected(item))
                    {
                        tree.SelectSingleItem(item);
                    }
                }
                e.Handled = true;
                tree.SignalSelectionChanged();
                tree.Focus();
            }
        }

        private static void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var item = FindAncestor<TreeViewItem>(e.OriginalSource as DependencyObject);
            var tree = sender as TreeView;
            if (item != null && tree != null)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (GetIsItemSelected(item) && GetJustChangedSelection(tree) != item)
                    {
                        tree.ToggleRandomSelection(item);
                    }
                }
                else if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    ;
                }
                else
                {
                    if (GetIsItemSelected(item))
                    {
                        tree.SelectSingleItem(item);
                    }
                }
                SetJustChangedSelection(tree, null);
                e.Handled = true;
                tree.SignalSelectionChanged();
                tree.Focus();
            }
        }

        private static void SelectSingleItem(this TreeView tree, TreeViewItem item)
        {
            DeselectSelfAndDescendants(tree);
            SetIsItemSelected(item, true);
            SetStartItem(tree, item);
        }

        private static void SignalSelectionChanged(this TreeView tree)
        {
            SetSelectedItems(tree, GetSelectedItems(tree).Cast<object>().ToList());
        }

        private static void ToggleRandomSelection(this TreeView tree, TreeViewItem item)
        {
            SetIsItemSelected(item, !GetIsItemSelected(item));
            if (GetStartItem(tree) == null)
            {
                if (GetIsItemSelected(item)) { SetStartItem(tree, item); }
            }
            else
            {
                if (GetSelectedItems(tree).Count == 0) { SetStartItem(tree, null); }
            }
        }
    }
}
