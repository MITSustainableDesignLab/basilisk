using System.Windows;
using System.Windows.Controls;

namespace Basilisk.Controls
{
    public class SimulationSettingTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var grid = container as FrameworkElement;
            var setting = item as SimulationSetting;
            if (grid != null && setting != null)
            {
                var template =
                    setting.ExposeAsComboBox ? grid.FindResource("EnumPropertyTemplate") :
                    setting.ExposeAsCheckbox ? grid.FindResource("BoolPropertyTemplate") :
                    setting.ShowMultivalueDescription ? grid.FindResource("MultiValueDescriptionTemplate") :
                    grid.FindResource("TextPropertyTemplate");
                return (DataTemplate)template;
            }
            return null;
        }
    }
}
