namespace WizardMaker.DataDomain.Models.JsonModel
{
    public abstract class BaseData
    {
        public string? Description { get; set; }
        public string? Source { get; set; }
        public int Page { get; set; }
    }
}
