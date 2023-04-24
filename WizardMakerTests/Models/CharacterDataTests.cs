namespace WizardMaker.DataDomain.Models.Tests;

[TestClass()]
public class CharacterDataTests
{
    [TestMethod()]
    public void SimpleCharacterDataSerializationTest()
    {
        CharacterManager cm = new(25, ArchAbility.LangEnglish);

        CharacterData cd = cm.RenderCharacterAsCharacterData();    
        Assert.IsNotNull(cd);

        string json = CharacterRenderer.SerializeCharacterData(cd);
        CharacterData cdDeserialized = CharacterRenderer.DeserializeCharacterData(json);

        Assert.IsNotNull(cdDeserialized);
        Assert.IsTrue(cd.IsSameSpec(cdDeserialized));
    }
}