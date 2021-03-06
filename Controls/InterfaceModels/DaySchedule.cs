﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.DaySchedule))]
    [DisplayName("day schedule")]
    [ComponentNamespace]
    public class DaySchedule : LibraryComponent
    {
        public DaySchedule()
        {
            Values = Enumerable.Repeat(0.0, 24).ToList();
        }

        public override string Category
        {
            get { return "Day"; }
            set { }
        }

        public override IEnumerable<LibraryComponent> AllReferencedComponents =>
            Enumerable.Empty<LibraryComponent>();

        public override bool IsCategoryNameMutable => false;

        [SimulationSetting]
        [DefaultValue("Fraction")]
        public string Type { get; set; } = "Fraction";

        public IList<double> Values { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new DaySchedule()
            {
                Type = Type,
                Values = Values
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator _)
        {
            var c = (DaySchedule)other;
            Type = c.Type;
            Values = c.Values?.ToList();
            CopyBasePropertiesFrom(c);
        }
    }
}
