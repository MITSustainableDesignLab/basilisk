using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for ComponentMetadataEditor.xaml
    /// </summary>
    public partial class ComponentMetadataEditor : UserControl
    {
        public ComponentMetadataEditor()
        {
            InitializeComponent();
        }

        public string Category
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        public static readonly DependencyProperty CategoryProperty =
            DependencyProperty.Register(
                nameof(Category),
                typeof(string),
                typeof(ComponentMetadataEditor),
                new FrameworkPropertyMetadata("uncategorized", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Comments
        {
            get { return (string)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        public static readonly DependencyProperty CommentsProperty =
            DependencyProperty.Register(
                nameof(Comments),
                typeof(string),
                typeof(ComponentMetadataEditor),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ComponentName
        {
            get { return (string)GetValue(ComponentNameProperty); }
            set { SetValue(ComponentNameProperty, value); }
        }

        public static readonly DependencyProperty ComponentNameProperty =
            DependencyProperty.Register(
                nameof(ComponentName),
                typeof(string),
                typeof(ComponentMetadataEditor),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string DataSource
        {
            get { return (string)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register(
                nameof(DataSource),
                typeof(string),
                typeof(ComponentMetadataEditor),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsCategoryReadOnly
        {
            get { return (bool)GetValue(IsCategoryReadOnlyProperty); }
            set { SetValue(IsCategoryReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsCategoryReadOnlyProperty =
            DependencyProperty.Register(
                nameof(IsCategoryReadOnly),
                typeof(bool),
                typeof(ComponentMetadataEditor));

        public bool IsNameReadOnly
        {
            get { return (bool)GetValue(IsNameReadOnlyProperty); }
            set { SetValue(IsNameReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsNameReadOnlyProperty =
            DependencyProperty.Register(
                nameof(IsNameReadOnly),
                typeof(bool),
                typeof(ComponentMetadataEditor));
    }
}
