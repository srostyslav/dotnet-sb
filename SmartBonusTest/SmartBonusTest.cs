using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SBonus;
using System;

namespace SmartBonusTest
{

    [TestClass]
    public class SmartBonus
    {
        static Store store = new Store("fa7cdd67-e973-4125-9990-9c50d61faa3a", "https://yoursbdomain.com/api/v2/");
        static string TestUserID = "";

        [TestMethod]
        public void TestUser()
        {
            Client client = store.GetClient(TestUserID);
            Assert.IsNotNull(client.Balance);
            Assert.IsNotNull(client.Phone);
        }

        [TestMethod]
        public void TestNomenclature()
        {
            var nomes = new List<object>();
            nomes.Add(new Nomenclature() { ID = "1", Name = "Shirts", IsCategory = true });
            nomes.Add(new Nomenclature()
            {
                ID = "2",
                Name = "Yellow shirt",
                Description = "Best quality",
                Image = "https://yoursite.com/products/yellow-shift.png",
                CategoryID = "1",  // Category reference
                Price = (float)699.99,
                Tags = new List<string>() { "3", "7" },  // List of tags
            });
            nomes.Add(new Nomenclature()
            {
                ID = "3",
                Name = "Blue shirt",
                Image = "https://yoursite.com/products/blue-shift-back.png,https://yoursite.com/products/blue-shift-front.png",
                CategoryID = "1",  // Category reference
                Price = (float)699.99,
                Tags = new List<string>() { "3", "6" },  // List of tags
            });
            nomes.Add(new Nomenclature()
            {
                ID = "4",
                Name = "black hat",
                CanBuy = true,
                IsHidden = false
            });

            store.SyncNomenclature(nomes);
        }

        [TestMethod]
        public void TestOrderConfig()
        {
            string orderUrl = "https://domain:port/api/order";
            string statusUrl = "https://domain:port/api/status";
            store.ConfigOrder(orderUrl, statusUrl, "really strong token of your store");
        }

        [TestMethod]
        public void TestChangeOrderStatus()
        {
            store.ChangeOrderStatus("fce887b6-b307-cc0f-309d-933db16e406b", OrderStatuses.Processing);
        }

        [TestMethod]
        public void TestGetDiscount()
        {
            List<object> items = new List<object>();
            items.Add(new NomenclatureItem("2", 10, (float)89.65));
            items.Add(new NomenclatureItem("3", (float)0.245, (float)23.9));

            var result = store.GetDiscount(TestUserID, items);
            Assert.IsNotNull(result.Withdrawn);
            Assert.IsNotNull(result.Accrued);
        }

        [TestMethod]
        public void TestConfirm()
        {
            List<object> items = new List<object>();
            items.Add(new NomenclatureItem("2", 10, (float)89.65));
            items.Add(new NomenclatureItem("3", (float)0.245, (float)23.9));
            // If client pay 900 change 2.36 you can accrued to smartbonus account
            var result = store.Confirm(TestUserID, items, Guid.NewGuid().ToString(), store.GetTimestamp(DateTime.UtcNow), 0, (float)2.36);
            Assert.IsNotNull(result.Accrued);
            Assert.IsNotNull(result.Discount);
           
            result = store.Confirm(TestUserID, items, Guid.NewGuid().ToString(), store.GetTimestamp(DateTime.UtcNow), 24);
            Assert.IsNotNull(result.Accrued);
            Assert.IsNotNull(result.Discount);
        }

        [TestMethod]
        public void TestDelete()
        {
            List<object> items = new List<object>();
            items.Add(new NomenclatureItem("2", 10, (float)89.65));
            items.Add(new NomenclatureItem("3", (float)0.245, (float)23.9));
            string id = Guid.NewGuid().ToString();
            store.Confirm(TestUserID, items, id, 0, 0);

            List<string> receipts = new List<string>();
            receipts.Add(id);
            Assert.AreEqual(store.DeleteReceipts(receipts), "Delete success");
        }

        [TestMethod]
        public void TestRefund()
        {
            List<object> items = new List<object>();
            items.Add(new NomenclatureItem("2", 10, (float)89.65));
            items.Add(new NomenclatureItem("3", (float)0.245, (float)23.9));
            string id = Guid.NewGuid().ToString();
            store.Confirm(TestUserID, items, id, 0, 0);

            items.Clear();
            items.Add(new RefundItem("2", 8));
            var result = store.RefundReceipt(Guid.NewGuid().ToString(), id, items);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestTags()
        {
            List<object> elements = new List<object>();
            elements.Add(new Tag("1", "Size", null, true));
            elements.Add(new Tag("2", "M", "1"));
            elements.Add(new Tag("3", "S", "1"));
            elements.Add(new Tag("5", "Red", "4"));
            elements.Add(new Tag("4", "Color", null, true));
            elements.Add(new Tag("6", "Blue", "4"));
            elements.Add(new Tag("7", "Yellow", "4"));

            Assert.AreEqual(store.SyncTags(elements), "Sync success");
        }
    }
}
