using Basilisk.Controls.Attributes;

namespace Basilisk.Controls.InterfaceModels
{
    [UseDefaultValuesOf(typeof(Core.GasMaterial))]
    [DisplayName("gas material")]
    public class GasMaterial : WindowMaterialBase
    {
        public override string Category
        {
            get { return "Gas"; }
            set { }
        }

        public override bool IsCategoryNameMutable => false;

        public string Type => Name;

        public override bool DirectlyReferences(LibraryComponent component) =>
            false;

        public override LibraryComponent Duplicate()
        {
            var res = new GasMaterial();
            res.CopyBasePropertiesFrom(this);
            return res;
        }
    }
}
