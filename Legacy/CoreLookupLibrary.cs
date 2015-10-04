using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core = Basilisk.Core;

namespace Basilisk.Legacy
{
    internal class CoreLookupLibrary : Core.Library
    {
        private IDictionary<string, Core.OpaqueMaterial> opaqueMaterialLookup;
        private IDictionary<string, Core.WindowMaterialBase> windowMaterialLookup;
        private IDictionary<string, Core.OpaqueConstruction> opaqueConstructionLookup;
        private IDictionary<string, Core.WindowConstruction> windowConstructionLookup;
        private IDictionary<string, Core.DaySchedule> dayScheduleLookup;
        private IDictionary<string, Core.WeekSchedule> weekScheduleLookup;
        private IDictionary<string, Core.YearSchedule> yearScheduleLookup;

        public CoreLookupLibrary() : base() { }

        public IDictionary<string, Core.OpaqueMaterial> OpaqueMaterialLookup
        {
            get
            {
                if (opaqueMaterialLookup == null)
                {
                    opaqueMaterialLookup = OpaqueMaterials.ToDictionary(m => m.Name);
                }
                return opaqueMaterialLookup;
            }
            set { opaqueMaterialLookup = value; }
        }

        public IDictionary<string, Core.WindowMaterialBase> WindowMaterialLookup
        {
            get
            {
                if (windowMaterialLookup == null)
                {
                    windowMaterialLookup =
                        GlazingMaterials
                        .Cast<Core.WindowMaterialBase>()
                        .Concat(GasMaterials)
                        .ToDictionary(m => m.Name);
                }
                return windowMaterialLookup;
            }
            set { windowMaterialLookup = value; }
        }

        public IDictionary<string, Core.OpaqueConstruction> OpaqueConstructionLookup
        {
            get
            {
                if (opaqueConstructionLookup == null)
                {
                    opaqueConstructionLookup = OpaqueConstructions.ToDictionary(m => m.Name);
                }
                return opaqueConstructionLookup;
            }
            set { opaqueConstructionLookup = value; }
        }

        public IDictionary<string, Core.WindowConstruction> WindowConstructionLookup
        {
            get
            {
                if (windowConstructionLookup == null)
                {
                    windowConstructionLookup = WindowConstructions.ToDictionary(m => m.Name);
                }
                return windowConstructionLookup;
            }
            set { windowConstructionLookup = value; }
        }

        public IDictionary<string, Core.DaySchedule> DayScheduleLookup
        {
            get
            {
                if (dayScheduleLookup == null)
                {
                    dayScheduleLookup = DaySchedules.ToDictionary(m => m.Name);
                }
                return dayScheduleLookup;
            }
            set { dayScheduleLookup = value; }
        }

        public IDictionary<string, Core.WeekSchedule> WeekScheduleLookup
        {
            get
            {
                if (weekScheduleLookup == null)
                {
                    weekScheduleLookup = WeekSchedules.ToDictionary(m => m.Name);
                }
                return weekScheduleLookup;
            }
            set { weekScheduleLookup = value; }
        }

        public IDictionary<string, Core.YearSchedule> YearScheduleLookup
        {
            get
            {
                if (yearScheduleLookup == null)
                {
                    yearScheduleLookup = YearSchedules.ToDictionary(m => m.Name);
                }
                return yearScheduleLookup;
            }
            set { yearScheduleLookup = value; }
        }
    }
}
