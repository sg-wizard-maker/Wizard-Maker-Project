using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel;

public class SpellData : BaseData
{
    public HermeticTechnique       PrimaryTechnique    { get; set; }
    public HermeticForm            PrimaryForm         { get; set; }
    public List<HermeticTechnique> RequisitesTechnique { get; set; } = new List<HermeticTechnique>();
    public List<HermeticForm>      RequisitesForms     { get; set; } = new List<HermeticForm>();

    public HermeticSpellRanges    Range    { get; set; }
    public HermeticSpellDurations Duration { get; set; }
    public HermeticSpellTargets   Target   { get; set; }

    public int SpellLevel    { get; set; }
    public int TargetSize    { get; set; }
    public int Complexity    { get; set; }
    public int EnhancingReqs { get; set; }

    public int    BaseLevel             { get; set; }
    public string BaseEffectDescription { get; set; } = "";
    public bool   Ritual                { get; set; }
}
