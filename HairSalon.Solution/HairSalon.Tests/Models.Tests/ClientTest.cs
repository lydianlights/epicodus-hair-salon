using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=rane_fields_test;";
        }
        public void Dispose()
        {
            Client.ClearAll();
        }

        [TestMethod]
        public void GetAll_DatabaseIsEmptyAtFirst_0()
        {
            List<Client> allClients = Client.GetAll();

            int result = allClients.Count;

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void Equals_BothHaveSameProperties_True()
        {
            Client client1 = new Client("Dude McBuff", "555-2833", 0);
            Client client2 = new Client("Dude McBuff", "555-2833", 0);

            Assert.AreEqual(client1, client2);
        }
        [TestMethod]
        public void Equals_BothDontHaveSameProperties_False()
        {
            Client client1 = new Client("Dude McBuff", "555-2833", 0);
            Client client2 = new Client("Meghan McMannicure", "555-6245", 0);

            Assert.AreNotEqual(client1, client2);
        }
        [TestMethod]
        public void Equals_ComparingDifferentObjects_False()
        {
            Client client1 = new Client("A Cat", "555-6369", 0);
            string client2 = "Meow";

            Assert.AreNotEqual(client1, client2);
        }
        [TestMethod]
        public void Save_SavesObjectToDatabase_ObjectIsSaved()
        {
            Client localClient = new Client("Meghan McMannicure", "555-6245", 0);
            localClient.Save();
            Client databaseClient = Client.GetAll()[0];

            Assert.AreEqual(localClient, databaseClient);
        }
        [TestMethod]
        public void Save_SavesMultipleObjectsToDatabase_ObjectsAreSaved()
        {
            Client localClient1 = new Client("Dude McBuff", "555-2833", 0);
            localClient1.Save();
            Client localClient2 = new Client("Meghan McMannicure", "555-6245", 0);
            localClient2.Save();
            List<Client> allLocalClients = new List<Client> {localClient1, localClient2};
            List<Client> allDatabaseClients = Client.GetAll();

            CollectionAssert.AreEqual(allLocalClients, allDatabaseClients);
        }
        [TestMethod]
        public void FindById_GetsSpecificObjectFromDatabase_Object()
        {
            Client localClient1 = new Client("Dude McBuff", "555-2833", 0);
            localClient1.Save();
            Client localClient2 = new Client("Meghan McMannicure", "555-6245", 0);
            localClient2.Save();
            Client localClient3 = new Client("A Cat", "555-6369", 0);
            localClient3.Save();
            Client databaseClient2 = Client.FindById((int)localClient2.Id);

            Assert.AreEqual(localClient2, databaseClient2);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindById_ObjectDoesntExistInDatabase_Exception()
        {
            Client databaseClient2 = Client.FindById(0);
        }
        [TestMethod]
        public void Update_UpdateObjectInDatabase_UpdatedObject()
        {
            Client initialClient = new Client("Dude Buff", "555-2833", 0);
            initialClient.Save();
            Client localUpdatedClient = new Client("Dude McBuff", "555-2833", 0);
            Client.UpdateAtId((int)initialClient.Id, localUpdatedClient);
            Client databaseUpdatedClient = Client.FindById((int)initialClient.Id);

            Assert.AreEqual(localUpdatedClient, databaseUpdatedClient);
        }
    }
}
