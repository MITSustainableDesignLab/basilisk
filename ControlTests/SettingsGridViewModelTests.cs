using System;
using System.ComponentModel;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Controls;

namespace Basilisk.ControlTests
{
    [TestClass]
    public class SettingsGridViewModelTests
    {
        [TestMethod]
        public void SettingGeneration_NoAttribute_NoSetting()
        {
            var source = new TestVM();
            var generated = new SettingsGridViewModel<TestVM>(source);
            Assert.IsFalse(generated.Settings.Any(s => s.PropertyName == "NotASetting"));
        }

        [TestMethod]
        public void Events_GeneratedOnSourceUpdate_PropertyChangedFires()
        {
            var source = new TestVM();
            var generated = new SettingsGridViewModel<TestVM>(source);
            var setting = generated.Settings.Single(s => s.PropertyName == "RealSetting");
            var fired = false;
            setting.PropertyChanged += (s, e) =>
                fired = String.IsNullOrEmpty(e.PropertyName) || e.PropertyName == "Value";
            source.RealSetting = 5.0;
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void Events_SourceOnGeneratedUpdate_PropertyChangedFires()
        {
            var source = new TestVM();
            var generated = new SettingsGridViewModel<TestVM>(source);
            var setting = generated.Settings.Single(s => s.PropertyName == "RealSetting");
            var fired = false;
            source.PropertyChanged += (s, e) =>
                fired = String.IsNullOrEmpty(e.PropertyName) || e.PropertyName == setting.PropertyName;
            setting.Value = 5.0;
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void Values_GeneratedValueChanged_SourceValueReflectsChange()
        {
            var source = new TestVM();
            var generated = new SettingsGridViewModel<TestVM>(source);
            var setting = generated.Settings.Single(s => s.PropertyName == "RealSetting");
            setting.Value = 5.0;
            Assert.AreEqual(setting.Value, source.RealSetting);
        }

        [TestMethod]
        public void Values_SourceValueChanged_GeneratedValueReflectsChange()
        {
            var source = new TestVM();
            var generated = new SettingsGridViewModel<TestVM>(source);
            var setting = generated.Settings.Single(s => s.PropertyName == "RealSetting");
            source.RealSetting = 5.0;
            Assert.AreEqual(source.RealSetting, setting.Value);
        }
    }
}
