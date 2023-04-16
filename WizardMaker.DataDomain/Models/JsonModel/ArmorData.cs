using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel
{
    public class ArmorData : BaseData
    {
        public EquipmentCost Cost     { get; set; }
        public int           Quantity { get; set; }
        public int           Weight   { get; set; }
        public int           Prot     { get; set; }
        public bool          Full     { get; set; }
        public bool          Equiped  { get; set; }
    }
}
