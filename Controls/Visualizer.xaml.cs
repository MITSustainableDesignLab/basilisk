using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Basilisk.Controls.InterfaceModels;

namespace Basilisk.Controls
{
    /// <summary>
    /// Interaction logic for Visualizer.xaml
    /// </summary>
    public partial class Visualizer : UserControl
    {
        public Visualizer()
        {
            InitializeComponent();
        }

        public bool IsAnythingVisualized
        {
            get { return (bool)GetValue(IsAnythingVisualizedProperty); }
            set { SetValue(IsAnythingVisualizedProperty, value); }
        }

        public static DependencyProperty IsAnythingVisualizedProperty =
            DependencyProperty.Register(
                nameof(IsAnythingVisualized),
                typeof(bool),
                typeof(Visualizer));

        public object VisualizedObject
        {
            get { return GetValue(VisualizedObjectProperty); }
            set { SetValue(VisualizedObjectProperty, value); }
        }

        public static DependencyProperty VisualizedObjectProperty =
            DependencyProperty.Register(
                nameof(VisualizedObject),
                typeof(object),
                typeof(Visualizer),
                new FrameworkPropertyMetadata((s, e) => ((Visualizer)s).DrawNewObject(e.NewValue)));

        private void DrawNewObject(object o)
        {
            IsAnythingVisualized = o != null && Draw((dynamic)o);
        }

        private bool Draw(object o) => false;

        private bool Draw(OpaqueMaterial c)
        {
            canvas.Background = Brushes.Red;
            return true;
        }

        private bool Draw(GlazingMaterial c)
        {
            canvas.Background = Brushes.Blue;
            return true;
        }

        private bool Draw(OpaqueConstruction c)
        {
            canvas.Background = Brushes.Green;
            return true;
        }
    }
}
