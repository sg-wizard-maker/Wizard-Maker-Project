using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMaker.DataDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Tests
{
    [TestClass()]
    public class CharacterDataTests
    {
        [TestMethod()]
        public void SimpleCharacterDataSerializationTest()
        {
            CharacterManager cm = new(25);

            CharacterData cd = cm.renderCharacterAsCharacterData();    
            Assert.IsNotNull(cd);

            string json = CharacterRenderer.serializeCharacterData(cd);
            CharacterData cdDeserialized = CharacterRenderer.deserializeCharacterData(json);

            Assert.IsNotNull(cdDeserialized);
            Assert.IsTrue(cd.IsSameSpec(cdDeserialized));
        }
    }
}