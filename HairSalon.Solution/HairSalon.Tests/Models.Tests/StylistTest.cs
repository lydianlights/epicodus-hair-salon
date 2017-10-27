using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class StylistTest
  {
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

        bool result = stylist1.Equals(stylist2);

        Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void Equals_BothDontHaveSameProperties_False()
    {
        Stylist stylist1 = new Stylist("Harry Styles", "555-4247", "ultrastyles42@yahoo.com");
        Stylist stylist2 = new Stylist("Katie Cutter", "555-2467", "xXxkatethegreatxXx@aol.com");

        bool result = stylist1.Equals(stylist2);

        Assert.AreEqual(false, result);
    }
  }
}
