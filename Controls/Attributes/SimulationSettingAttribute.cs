﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Controls.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SimulationSettingAttribute : Attribute
    {
        public SimulationSettingAttribute()
        {
            SettingType = SettingType.Unspecified;
        }

        public string DisplayName { get; set; }
        public Type EnumType { get; set; }
        public bool MustBePositive { get; set; }
        public SettingType SettingType { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }

        public string[] EnumChoices
        {
            get
            {
                return EnumType != null ? Enum.GetNames(EnumType) : null;
            }
        }
    }
}
