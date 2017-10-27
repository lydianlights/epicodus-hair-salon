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
  }
}
