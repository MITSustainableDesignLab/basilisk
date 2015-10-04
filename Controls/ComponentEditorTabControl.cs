using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    public class ComponentEditorTabControl : TabControl
    {
        public ComponentEditorTabControl()
        {
            SelectionChanged += OnSelectionChanged;
            Initialized += OnInitialized;
        }

        private void EstablishComponentsPropagation(DependencyObject propagateFrom)
        {
            if (GetComponentsToPropagateUpward(propagateFrom) == null)
            {
                var contentChild = propagateFrom as ContentControl;
                if (contentChild != null)
                {
                    var content = contentChild.Content as DependencyObject;
                    if (content != null && GetComponentsToPropagateUpward(content) != null)
                    {
                        propagateFrom = content;
                    }
                }
            }
            // This is here because tab controls will be notified about selection changes
            // if *any* subtab changes, and sometimes those subtabs will not have any
            // directly-assigned propagation components (because their parents are taking)
            // care of it. In this case the propagation set will always be null. I think
            // it would be better to just ignore links that aren't directly from child to
            // parent, but this is working for now.
            if (GetComponentsToPropagateUpward(propagateFrom) == null) { return; }
            var binding = new Binding()
            {
                Source = propagateFrom,
                Path = new PropertyPath(ComponentsToPropagateUpwardProperty)
            };
            SetBinding(ComponentsToPropagateUpwardProperty, binding);
        }

        private void EstablishComponentTypePropagation(DependencyObject propagateFrom)
        {
            if (GetComponentTypeToPropagateUpward(propagateFrom) == null)
            {
                var contentChild = propagateFrom as ContentControl;
                if (contentChild != null)
                {
                    var content = contentChild.Content as DependencyObject;
                    if (content != null && GetComponentTypeToPropagateUpward(content) != null)
                    {
                        propagateFrom = content;
                    }
                }
            }
            var binding = new Binding()
            {
                Source = propagateFrom,
                Path = new PropertyPath(ComponentTypeToPropagateUpwardProperty)
            };
            SetBinding(ComponentTypeToPropagateUpwardProperty, binding);
        }

        private void EstablishPropagationBindings(DependencyObject propagateFrom)
        {
            EstablishComponentsPropagation(propagateFrom);
            EstablishComponentTypePropagation(propagateFrom);
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                var first = (DependencyObject)Items[0];
                EstablishPropagationBindings(first);
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Source is ComponentEditorTabControl)) { return; }
            if (e.AddedItems.Count == 0) { return; }
            var added = e.AddedItems.Cast<DependencyObject>().Single();
            EstablishPropagationBindings(added);
        }

        public ICollection<LibraryComponent> ComponentsToPropagateUpward
        {
            get { return (ICollection<LibraryComponent>)GetValue(ComponentsToPropagateUpwardProperty); }
            set { SetValue(ComponentsToPropagateUpwardProperty, value); }
        }

        public Type ComponentTypeToPropagateUpward
        {
            get { return (Type)GetValue(ComponentTypeToPropagateUpwardProperty); }
            set { SetValue(ComponentTypeToPropagateUpwardProperty, value); }
        }

        public static ICollection<LibraryComponent> GetComponentsToPropagateUpward(DependencyObject target)
        {
            return (ICollection<LibraryComponent>)target.GetValue(ComponentsToPropagateUpwardProperty);
        }

        public static void SetComponentsToPropagateUpward(DependencyObject target, ICollection<LibraryComponent> value)
        {
            target.SetValue(ComponentsToPropagateUpwardProperty, value);
        }

        public static readonly DependencyProperty ComponentsToPropagateUpwardProperty =
            DependencyProperty.RegisterAttached(
                nameof(ComponentsToPropagateUpward),
                typeof(ICollection<LibraryComponent>),
                typeof(ComponentEditorTabControl),
                new FrameworkPropertyMetadata((s, e) =>
                {
                    var parentControl = s as ComponentEditorTabControl;
                    if (parentControl != null) { parentControl.CurrentComponents = (ICollection<LibraryComponent>)e.NewValue; }
                }));

        public static Type GetComponentTypeToPropagateUpward(DependencyObject target)
        {
            return (Type)target.GetValue(ComponentTypeToPropagateUpwardProperty);
        }

        public static void SetComponentTypeToPropagateUpward(DependencyObject target, Type value)
        {
            target.SetValue(ComponentTypeToPropagateUpwardProperty, value);
        }

        public static readonly DependencyProperty ComponentTypeToPropagateUpwardProperty =
            DependencyProperty.RegisterAttached(
                nameof(ComponentTypeToPropagateUpward),
                typeof(Type),
                typeof(ComponentEditorTabControl),
                new FrameworkPropertyMetadata((s, e) =>
                {
                    var parentControl = s as ComponentEditorTabControl;
                    if (parentControl != null) { parentControl.CurrentComponentType = (Type)e.NewValue; }
                }));

        public ICollection<LibraryComponent> CurrentComponents
        {
            get { return (ICollection<LibraryComponent>)GetValue(CurrentComponentsProperty); }
            set { SetValue(CurrentComponentsProperty, value); }
        }

        public static readonly DependencyProperty CurrentComponentsProperty =
            DependencyProperty.Register(
                nameof(CurrentComponents),
                typeof(ICollection<LibraryComponent>),
                typeof(ComponentEditorTabControl));

        public Type CurrentComponentType
        {
            get { return (Type)GetValue(CurrentComponentTypeProperty); }
            set { SetValue(CurrentComponentTypeProperty, value); }
        }

        public static readonly DependencyProperty CurrentComponentTypeProperty =
            DependencyProperty.Register(
                nameof(CurrentComponentType),
                typeof(Type),
                typeof(ComponentEditorTabControl));
    }
}
