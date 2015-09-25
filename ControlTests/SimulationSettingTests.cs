using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Basilisk.Controls;

namespace Basilisk.ControlTests
{
    [TestClass]
    public class SimulationSettingTests
    {
        [TestMethod]
        public void DefaultValue_UnspecifiedEnum_UsesFirstChoice()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var setting = wrapped.Settings.Single(s => s.PropertyName == "EnumSetting");
            Assert.AreEqual(Enum.GetNames(typeof(TestVM.TestEnum))[0], setting.Value.ToString());
        }

        [TestMethod]
        public void DisplayMode_ImplicitEnum_AsComboBox()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var setting = wrapped.Settings.Single(s => s.PropertyName == "EnumSetting");
            Assert.IsTrue(setting.ExposeAsComboBox);
        }

        [TestMethod]
        public void DisplayName_NoneSpecified_PropertyNameUsed()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var settings = wrapped.Settings.Single(s => s.PropertyName == "StringSetting");
            Assert.AreEqual("StringSetting", settings.DisplayName);
        }

        [TestMethod]
        public void DisplayName_Specified_Set()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var settings = wrapped.Settings.Single(s => s.PropertyName == "RealSetting");
            Assert.AreEqual("Real Setting", settings.DisplayName);
        }

        [TestMethod]
        public void SettingTypeInference_Integer_InferredCorrectly()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var setting = wrapped.Settings.Single(s => s.PropertyName == "IntegerSetting");
            Assert.AreEqual(SettingType.Integer, setting.SettingType);
        }

        [TestMethod]
        public void SettingTypeInference_Double_InferredCorrectly()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var setting = wrapped.Settings.Single(s => s.PropertyName == "RealSetting");
            Assert.AreEqual(SettingType.Real, setting.SettingType);
        }

        [TestMethod]
        public void SettingTypeInference_Enum_InferredCorrectly()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var setting = wrapped.Settings.Single(s => s.PropertyName == "EnumSetting");
            IStructuralEquatable expected = Enum.GetNames(typeof(TestVM.TestEnum));
            IStructuralEquatable actual = setting.Choices;
            Assert.IsTrue(expected.Equals(actual, StructuralComparisons.StructuralEqualityComparer));
        }

        [TestMethod]
        public void SettingTypeInference_String_InferredCorrectly()
        {
            var vm = new TestVM();
            var wrapped = new SettingsGridViewModel<TestVM>(vm);
            var setting = wrapped.Settings.Single(s => s.PropertyName == "StringSetting");
            Assert.AreEqual(SettingType.String, setting.SettingType);
        }
    }
}
