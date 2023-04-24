using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel;

public class WeaponData : BaseData
{
    public EquipmentCost Cost     { get; set; }
    public int           Quantity { get; set; }
    public int           Weight   { get; set; }
    public int           Init     { get; set; }
    public int           Atk      { get; set; }
    public int           Def      { get; set; }
    public int           Dmg      { get; set; }
    public int           Load     { get; set; }
}
