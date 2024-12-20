using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boogle_Dennery_Degioanni_TDG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Dennery_Degioanni_TDG.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void ContainTest()
        {
            string mot = "maison";
            Joueur j1 = new Joueur("Hugo", "FR");
            string[] tab = new string[1];

            bool test = j1.Contain(mot);


            Assert.AreEqual(false, test);
        }
    }
}