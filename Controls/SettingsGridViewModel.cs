using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls
{
    public class SettingsGridViewModel<SourceVMT> : INotifyPropertyChanged
    {
        private readonly SourceVMT source;

        public SettingsGridViewModel(SourceVMT source)
        {
            PropertyChanged += (s, e) => { };
            this.source = source;
            var sourceVM = source as INotifyPropertyChanged;
            if (sourceVM != null) { sourceVM.PropertyChanged += OnSourceUpdated; }
            Settings = new SettingsCollection();
            RefreshSettingsList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsCollection Settings { get; private set; }

        public void RefreshSettingsList()
        {
            var settings =
                source
                .GetType()
                .GetProperties()
                .Select(prop => new { Prop = prop, Att = prop.GetCustomAttribute<SimulationSettingAttribute>() })
                .Where(x => x.Att != null)
                .Select(
                    x =>
                    {
                        var displayName = x.Att.DisplayName == null ? x.Prop.Name : x.Att.DisplayName;
                        return new SimulationSetting(source, x.Prop, displayName);
                    });
            Settings = new SettingsCollection();
            foreach (var s in settings) { Settings.Add(s); }
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(Settings)));
        }

        private void OnSourceUpdated(object sender, PropertyChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.PropertyName))
            {
                foreach (var s in Settings) { s.Update(); }
            }
            else
            {
                SimulationSetting setting;
                if (Settings.TryGetValue(e.PropertyName, out setting))
                {
                    setting.Update();
                }
            }
        }
    }
}
