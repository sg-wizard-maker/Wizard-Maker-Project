using WizardMaker.DataDomain.Models.Enums;

namespace WizardMaker.DataDomain.Models.JsonModel
{
    public class GameObj : Dictionary<string, Root>
    {
    }

    public class Root
    {
        public string?  Name { get; set; }
        public ObjType  Type { get; set; }
        public BaseData Data { get; set; }
    }
}
