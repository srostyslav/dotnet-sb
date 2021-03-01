using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

namespace SBonus
{

    public class Store
    {
        public string StoreID { set; get; }

        public Store(string store, string rootUrl)
        {
            StoreID = store;
            Http.rootUrl = rootUrl;
        }

        /// <summary>
        ///     If client exists in smartbonus app return its instance
        ///     userId - phone or scanned key from smartbonus app
        /// </summary>
        /// <returns>instance of Client</returns>
        public Client GetClient(string userID)
        {
            string resp = Http.Get($"user/phone?store={StoreID}&user_id={userID}");
            return JsonConvert.DeserializeObject<Client>(resp);
        }

        /// <summary>
        ///     Sync your catalog to smartbonus app
        ///     warning: ensure that count of elements has to be less than or equal 500 in a request
        /// </summary>
        public void SyncNomenclature(List<object> nomes)
        {
            if (nomes.Count > 500)
            {
                throw new Exception("Length of nomenclatures must be less or equal than 500 elements");
            } 
            else if (nomes.Count == 0)
            {
                throw new Exception("No element found");
            }

            JObject body = new JObject();
            body.Add("elements", Http.ToJsonArray(nomes));
            body.Add("store", StoreID);

            string resp = JsonConvert.DeserializeObject<string>(Http.Post("sync/nomenclature", body));
            if (resp != "Sync success")
            {
                throw new Exception(resp);
            }
        }

        /// <summary>
        ///     Config order hook
        ///     when user created order in smartbonus we send it to your api orderUrl with body Order struct.
        /// </summary>
        /// <param name="orderUrl">your api that wait new order from smartbonus app</param>
        /// <param name="statusUrl">your api that wait new status of order: receive body StatusBody</param>
        /// <param name="token">your unique identifier of store: your receive it in orderUrl & statusUrl hooks in field "store"</param>
        public void ConfigOrder(string orderUrl, string statusUrl, string token)
        {
            var body = new JObject();
            body.Add("order_url", orderUrl);
            body.Add("status_url", statusUrl);
            body.Add("token", token);
            body.Add("store", StoreID);

            string resp = JsonConvert.DeserializeObject<string>(Http.Post("order/config", body));
            if (resp != null)
            {
                throw new Exception(resp);
            }
        }

        /// <summary>
        ///     Change status of order that created in smartbonus app
        ///     If status changed client receive push notification about it
        /// </summary>
        /// <param name="orderID">Identifier of order</param>
        /// <param name="status">Status of order</param>
        public void ChangeOrderStatus(string orderID, OrderStatuses status)
        {
            JObject body = new JObject();
            body.Add("store", StoreID);
            body.Add("order_id", orderID);
            body.Add("status", (int)status);

            string resp = JsonConvert.DeserializeObject<string>(Http.Post("order/status", body));
            if (resp != null) 
            {
                throw new Exception(resp);
            }
        }

        /// <summary>
        ///     Receipt discount method
        /// </summary>
        /// <param name="userID">Phone or sanned key from smartbonus app</param>
        /// <param name="items">products of receipt</param>
        /// <param name="date">Date of receipt (optional)</param>
        /// <param name="withdrawn">Amount of money that cashier want to withdraw from client account (optional)</param>
        /// <returns>instance of ReceiptResult</returns>
        public ReceiptResult GetDiscount(string userID, List<object> items, Int32 date = 0, float withdrawn = 0)
        {
            JObject body = new JObject();
            body.Add("user_id", userID);
            if (date > 0)
            {
                body.Add("date", date);
            }
            if (withdrawn > 0)
            {
                body.Add("withdrawn", withdrawn);
            }
            body.Add("store", StoreID);
            body.Add("receipt", Http.ToJsonArray(items));

            return JsonConvert.DeserializeObject<ReceiptResult>(Http.Post("receipt/discount", body));
        }

        /// <summary>
        ///     Receipt confirmation
        /// </summary>
        /// <param name="userID">Phone or sanned key from smartbonus app</param>
        /// <param name="items">products of receipt</param>
        /// <param name="id">Unique receipt identifier</param>
        /// <param name="date">Date of receipt (optional)</param>
        /// <param name="discount">Amount of discount that received from DiscountReceipt method.</param>
        /// <param name="change">Rest of money that will accrue to smartbonus account</param>
        /// <returns>instance of ReceiptResult</returns>
        public ReceiptResult Confirm(string userID, List<object> items, string id, Int32 date, float discount, float change = 0)
        {
            JObject body = new JObject();
            body.Add("user_id", userID);
            if (date > 0)
            {
                body.Add("date", date);
            }
            body.Add("discount", discount);
            body.Add("list", Http.ToJsonArray(items));
            body.Add("remote_id", id);
            body.Add("accrued", change);
            body.Add("store", StoreID);

            return JsonConvert.DeserializeObject<ReceiptResult>(Http.Post("receipt/confirm", body));
        }

        /// <summary>
        ///     Delete previous confirmed receipts
        /// </summary>
        /// <param name="receipts">List of receipt identifiers</param>
        /// <returns>Smartbonus response</returns>
        public string DeleteReceipts(List<string> receipts)
        {
            if (receipts.Count > 100)
            {
                throw new Exception("Length of receipts must be less or equal than 100 elements");
            }
            else if (receipts.Count == 0)
            {
                throw new Exception("No element found");
            }

            JObject body = new JObject();
            JArray elements = new JArray();
            foreach(string receipt in receipts)
            {
                JObject el = new JObject();
                el.Add("remote_id", receipt);
                elements.Add(el);
            }

            body.Add("store", StoreID);
            body.Add("elements", elements);

            string resp = JsonConvert.DeserializeObject<string>(Http.Post("delete/receipt", body));
            if (!resp.StartsWith("Delete success"))
            {
                throw new Exception(resp);
            }

            return resp;
        }

        /// <summary>
        ///     Refund products of receipt
        /// </summary>
        /// <param name="id">identifier of your refund receipt</param>
        /// <param name="receiptID">identifier of your sell receipt</param>
        /// <param name="items">list of refund products</param>
        /// <returns>List of refunded items</returns>
        public List<RefundItemResult> RefundReceipt(string id, string receiptID, List<object> items)
        {
            JObject body = new JObject();
            body.Add("store", StoreID);
            body.Add("refund_id", id);
            body.Add("remote_id", receiptID);
            body.Add("list", Http.ToJsonArray(items));

            string resp = Http.Post("refund/receipt", body);
            return JsonConvert.DeserializeObject<List<RefundItemResult>>(resp);
        }

        /// <summary>
        ///     Sync list of tags
        ///     warning: ensure that count of elements has to be less than or equal 500 in a request
        /// </summary>
        /// <param name="tags">List of tag instances</param>
        /// <returns>Smartbonus response</returns>
        public string SyncTags(List<object> tags)
        {
            if (tags.Count > 100)
            {
                throw new Exception("Length of receipts must be less or equal than 500 elements");
            } 
            else if (tags.Count == 0)
            {
                throw new Exception("No element found");
            }

            JObject body = new JObject();
            body.Add("store", StoreID);
            body.Add("elements", Http.ToJsonArray(tags));

            string resp = JsonConvert.DeserializeObject<string>(Http.Post("sync/tag", body));
            if (!resp.StartsWith("Sync success"))
            {
                throw new Exception(resp);
            }

            return resp;
        }

        public Int32 GetTimestamp(DateTime date)
        {
            return (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }

}
