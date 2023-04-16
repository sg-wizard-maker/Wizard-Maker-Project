using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel
{
    public class VirtueFlawData : BaseData
    {
        public Category Type   { get; set; }
        public VFImpact Impact { get; set; }
    }
}
