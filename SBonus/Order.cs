using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace SBonus
{
    public enum OrderStatuses
    {
        New = 0,
        PaymentPending = 1,
        PaymentCanceled = 2,
        Processing = 3,
        AwaitingShipment = 4,
        AwaitingPickup = 5,
        Completed = 6,
        Canceled = 7,
        Refunded = 8
    }

    /// <summary>element of products in Order</summary>
    /// <param name="ID">your nomenclature identifier</param>
    /// <param name="Price">price of product</param>
    /// <param name="Quantity">quantity of product </param>
    public class OrderProduct
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("amount")]
        public float Price { get; set; }

        [JsonProperty("quantity")]  
        public float Quantity { get; set; }
    }

    /// <summary>element of statuses in Order</summary>
    /// <param name="Date">date of status creation</param>
    /// <param name="Status">one of OrderStatuses</param>
    public class OrderStatus
    {
        [JsonProperty("date_unix")]
        public Int32 Date { get; set; }

        [JsonProperty("status")]
        public OrderStatuses Status { get; set; }
    }

    /// <summary>send new order that created in smartbonus to your api after webhook is configured</summary>
    /// <param name="StoreID">smartbonus store identifier</param>
    /// <param name="ID">unique identifier of order in smartbonus</param>
    /// <param name="Code">number of order for client</param>
    /// <param name="UserID">client identifier that has to be used to sync receipt</param>
    /// <param name="Phone">phone number of client</param>
    /// <param name="UserName">name of client</param>
    /// <param name="Amount">amount for pay</param>
    /// <param name="Currency">ISO 4217: UAH, USD, EUR</param>
    /// <param name="Date">date of order</param>
    /// <param name="IsPaid">order paid online by client</param>
    /// <param name="ProductsAmount">amount of products</param>
    /// <param name="DeliveryCost">cost of delivery</param>
    /// <param name="Discount">amount of discount</param>
    /// <param name="Products">List of products</param>
    /// <param name="Statuses">List of statuses</param>
    /// <param name="Comment">client comment</param>
    /// <param name="DeliveryType">type of delivery</param>
    /// <param name="DeliveryAddress">delivery address</param>
    /// <param name="DeliveryTime">delivery time</param>
    public class Order
    {
        [JsonProperty("store")]
        public string StoreID { set; get; }

        [JsonProperty("remote_id")]
        public string ID { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("user_id")]
        public string UserID { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("date_unix")]
        public Int32 Date { get; set; }

        [JsonProperty("is_paid")]
        public bool IsPaid { get; set; }

        [JsonProperty("products_amount")]
        public float ProductsAmount { get; set; }

        [JsonProperty("delivery_cost")]
        public float DeliveryCost { get; set; }

        [JsonProperty("discount")]
        public float Discount { get; set; }

        [JsonProperty("products")]
        public List<OrderProduct> Products { get; set; }

        [JsonProperty("statuses")]
        public List<OrderStatus> Statuses { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("delivery")]
        public string DeliveryType { get; set; }

        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("deliveryTime")]
        public string DeliveryTime { get; set; }
    }
}
