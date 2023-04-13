using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel
{
    public class SpellData : BaseData
    {
        public Technique PrimaryTechnique { get; set; }
        public Form PrimaryForm { get; set; }
        public List<Technique> RequisitesTechnique { get; set; }
        public List<Form> RequisitesForms { get; set; }

        public SpellRange Range { get; set; }
        public SpellDuration Duration { get; set; }
        public SpellTarget Target { get; set; }

        public int SpellLevel { get; set; }
        public int TargetSize { get; set; }
        public int Complexity { get; set; }
        public int EnhancingReqs { get; set; }

        public int BaseLevel { get; set; }
        public string BaseEffectDescription { get; set; }
        public bool Ritual { get; set; }
    }
}
