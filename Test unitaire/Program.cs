using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_unitaire
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]

        public void TestMethodContain()
        {
            string mot = "maison";
            Joueur 1 = new Joueur("Hugo");
            string[] tab = new string[1];
            bool test = j1.Contain(mot,tab);
            Assert.AreEqual(false, test);
        }
        
    }
}
