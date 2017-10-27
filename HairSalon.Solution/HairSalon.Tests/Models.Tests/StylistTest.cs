using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=rane_fields_test;";
        }
        public void Dispose()
        {
            Stylist.ClearAll();
        }

        [TestMethod]
        public void GetAll_DatabaseIsEmptyAtFirst_0()
        {
            List<Stylist> allStylists = Stylist.GetAll();

            int result = allStylists.Count;

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_BothHaveSameProperties_True()
        {
            Stylist stylist1 = new Stylist("Barry the Chopper", "555-3622", "experiment66@amestrismail.com");
            Stylist stylist2 = new Stylist("Barry the Chopper", "555-3622", "experiment66@amestrismail.com");

            Assert.AreEqual(stylist1, stylist2);
        }
        [TestMethod]
        public void Equals_BothDontHaveSameProperties_False()
        {
            Stylist stylist1 = new Stylist("Harry Styles", "555-4247", "ultrastyles42@yahoo.com");
            Stylist stylist2 = new Stylist("Katie Cutter", "555-2467", "xXxkatethegreatxXx@aol.com");

            Assert.AreNotEqual(stylist1, stylist2);
        }
        [TestMethod]
        public void Equals_ComparingDifferentEntrys_False()
        {
            Stylist stylist1 = new Stylist("Barry the Chopper", "555-3622", "experiment66@amestrismail.com");
            int stylist2 = 66;

            Assert.AreNotEqual(stylist1, stylist2);
        }
        [TestMethod]
        public void Save_SavesEntryToDatabase_EntryIsSaved()
        {
            Stylist localStylist = new Stylist("Harry Styles", "555-4247", "ultrastyles42@yahoo.com");
            localStylist.Save();
            Stylist databaseStylist = Stylist.GetAll()[0];

            Assert.AreEqual(localStylist, databaseStylist);
        }
        [TestMethod]
        public void Save_SavesMultipleEntrysToDatabase_EntrysAreSaved()
        {
            Stylist localStylist1 = new Stylist("Harry Styles", "555-4247", "ultrastyles42@yahoo.com");
            localStylist1.Save();
            Stylist localStylist2 = new Stylist("Katie Cutter", "555-2467", "xXxkatethegreatxXx@aol.com");
            localStylist2.Save();
            List<Stylist> allLocalStylists = new List<Stylist> {localStylist1, localStylist2};
            List<Stylist> allDatabaseStylists = Stylist.GetAll();

            CollectionAssert.AreEqual(allLocalStylists, allDatabaseStylists);
        }
        [TestMethod]
        public void FindById_GetsSpecificEntryFromDatabase_Entry()
        {
            Stylist localStylist1 = new Stylist("Harry Styles", "555-4247", "ultrastyles42@yahoo.com");
            localStylist1.Save();
            Stylist localStylist2 = new Stylist("Katie Cutter", "555-2467", "xXxkatethegreatxXx@aol.com");
            localStylist2.Save();
            Stylist localStylist3 = new Stylist("Barry the Chopper", "555-3622", "experiment66@amestrismail.com");
            localStylist3.Save();
            Stylist databaseStylist2 = Stylist.FindById((int)localStylist2.Id);

            Assert.AreEqual(localStylist2, databaseStylist2);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindById_EntryDoesntExistInDatabase_Exception()
        {
            Stylist databaseStylist2 = Stylist.FindById(0);
        }
    }
}
