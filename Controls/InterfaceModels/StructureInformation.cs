#nullable enable
using Basilisk.Controls.Attributes;
using Basilisk.Controls.Extensions;
using Basilisk.Controls.InterfaceModels.AdvancedStructuralModeling;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.StructureInformation))]
    [DisplayName("structure definition")]
    [ComponentNamespace]
    public class StructureInformation : ConstructionBase
    {
        public StructureInformation()
        {
            AdvancedModel = (AdvancedStructuralModel)typeof(AdvancedStructuralModel).CreateComponentWithDefaults();
        }

        public AdvancedStructuralModel AdvancedModel { get; set; }

        public ObservableCollection<MassRatios> MassRatios { get; set; } = new ObservableCollection<MassRatios>();

        public bool UseAdvancedModel { get; set; }

        public override IEnumerable<LibraryComponent> AllReferencedComponents
        {
            get
            {
                var massRatioMaterials =
                    MassRatios
                    .Select(mr => mr.Material)
                    .Where(m => m != null);

                var advancedModelMaterials = UseAdvancedModel
                    ? AdvancedModel.ConstructionSystems.AllReferencedComponents
                    : Enumerable.Empty<LibraryComponent>();

                var direct = massRatioMaterials.Concat(advancedModelMaterials).ToList();

                var indirect = direct.SelectMany(m => m.AllReferencedComponents);
                return direct.Concat(indirect);
            }
        }

        public override bool DirectlyReferences(LibraryComponent component) =>
            AllReferencedComponents.Contains(component);

        public override LibraryComponent Duplicate()
        {
            var res = new StructureInformation()
            {
                AdvancedModel = new AdvancedStructuralModel
                {
                    ColumnWallSpacing = new ColumnWallSpacingSettings
                    {
                        PrimarySpan = AdvancedModel.ColumnWallSpacing.PrimarySpan,
                        SecondarySpan = AdvancedModel.ColumnWallSpacing.SecondarySpan
                    }
                },

                MassRatios = new ObservableCollection<MassRatios>(MassRatios.Select(mr => mr.Duplicate())),
                UseAdvancedModel = UseAdvancedModel
            };
            res.CopyBasePropertiesFrom(this);
            return res;
        }

        public override void OverwriteWith(LibraryComponent other, ComponentCoordinator coord)
        {
            var c = (StructureInformation)other;
            var mrs =
                c
                .MassRatios
                .Select(mr => new MassRatios()
                {
                    NormalRatio = mr.NormalRatio,
                    HighLoadRatio = mr.HighLoadRatio,
                    Material = coord.GetWithSameName(mr.Material)
                });
            MassRatios = new ObservableCollection<MassRatios>(mrs);
            UseAdvancedModel = c.UseAdvancedModel;
            CopyBasePropertiesFrom(c);
        }
    }
}
