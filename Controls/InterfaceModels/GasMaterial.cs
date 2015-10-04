using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.GasMaterial))]
    [ImmutableCategoryName]
    public class GasMaterial : WindowMaterialBase
    {
        static GasMaterial()
        {
            Mapper
                .CreateMap<Core.GasMaterial, GasMaterial>()
                .IncludeBase<Core.MaterialBase, MaterialBase>();
        }

        public override string Category
        {
            get { return "Gas"; }
            set { }
        }

        [SimulationSetting]
        public string Type { get; set; }
    }
}
