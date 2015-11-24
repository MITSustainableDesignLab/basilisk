using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.GasMaterial))]
    [ImmutableCategoryName]
    [DisplayName("gas material")]
    public class GasMaterial : WindowMaterialBase
    {
        public override string Category
        {
            get { return "Gas"; }
            set { }
        }

        [SimulationSetting]
        public string Type { get; set; }

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new GasMaterial() { Type = Type };
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
