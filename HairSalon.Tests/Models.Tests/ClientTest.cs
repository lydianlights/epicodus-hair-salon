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
        public void Equals_ComparingDifferentEntrys_False()
        {
            Client client1 = new Client("A Cat", "555-6369", 0);
            string client2 = "Meow";

            Assert.AreNotEqual(client1, client2);
        }
        [TestMethod]
        public void Save_SavesEntryToDatabase_EntryIsSaved()
        {
            Client localClient = new Client("Meghan McMannicure", "555-6245", 0);
            localClient.Save();
            Client databaseClient = Client.GetAll()[0];

            Assert.AreEqual(localClient, databaseClient);
        }
        [TestMethod]
        public void Save_SavesMultipleEntrysToDatabase_EntrysAreSaved()
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
        public void FindById_GetsSpecificEntryFromDatabase_Entry()
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
        public void FindById_EntryDoesntExistInDatabase_Exception()
        {
            Client updatedClient = Client.FindById(0);
        }
        [TestMethod]
        public void UpdateAtId_UpdateEntryInDatabase_UpdatedEntry()
        {
            Client initialClient = new Client("Dude Buff", "555-2833", 0);
            initialClient.Save();
            Client localUpdatedClient = new Client("Dude McBuff", "555-2833", 0);
            Client.UpdateAtId((int)initialClient.Id, localUpdatedClient);
            Client databaseUpdatedClient = Client.FindById((int)initialClient.Id);

            Assert.AreEqual(localUpdatedClient, databaseUpdatedClient);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateAtId_EntryDoesntExistInDatabase_Exception()
        {
            Client updatedClient = new Client("A Cat", "555-6369", 0);
            Client.UpdateAtId(0, updatedClient);
        }
        [TestMethod]
        public void RemoveAtId_RemovesEntryFromDatabase_EntryRemoved()
        {
            Client localClient1 = new Client("Dude McBuff", "555-2833", 0);
            localClient1.Save();
            Client localClient2 = new Client("Meghan McMannicure", "555-6245", 0);
            localClient2.Save();
            Client localClient3 = new Client("A Cat", "555-6369", 0);
            localClient3.Save();
            Client.RemoveAtId((int)localClient2.Id);
            List<Client> remainingLocalClients = new List<Client> {localClient1, localClient3};
            List<Client> remainingDatabaseClients = Client.GetAll();

            CollectionAssert.AreEqual(remainingLocalClients, remainingDatabaseClients);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveAtId_EntryDoesntExistInDatabase_Exception()
        {
            Client.RemoveAtId(0);
        }
    }
}
