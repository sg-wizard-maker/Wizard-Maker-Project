using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel;

public class VirtueFlawData : BaseData
{
    public VirtueFlawAbilityCategory Type   { get; set; }
    public VirtueFlawQualityCost     Impact { get; set; }
}
