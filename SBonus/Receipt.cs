using System.Collections.Generic;
using Newtonsoft.Json;

namespace SBonus
{
    /// <summary>Smartbonus modules that accrued/withdrawn bonuses or added discount</summary>
    /// <param name="ID">identifier of smartbonus module</param>
    /// <param name="Type">type of object: subscriber_module|discount_module|coupon_module</param>
    /// <param name="Accrued">amount of accrued bonuses</param>
    /// <param name="Immediate">amount of immediate discount</param>
    /// <param name="Withdrawn">amount of withdrawn bonuses</param>
    /// <param name="ModuleType">type of smartbonus module</param>
    /// <param name="Name">title of module</param>
    public class ExecutedModule
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("accrued_bonus")]
        public float Accrued { get; set; }

        [JsonProperty("immediate_bonus")]
        public float Immediate { get; set; }

        [JsonProperty("withdrawn_bonus")]
        public float Withdrawn { get; set; }

        [JsonProperty("module_type")]
        public string ModuleType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    /// <summary>List of executed modules</summary>
    public class AnalyticObject
    {
        [JsonProperty("executed_modules")]
        public List<ExecutedModule> ExecutedModules { get; set; }
    }

    /// <summary>Item of receipt response</summary>
    /// <param name="ID">your product identifier</param>
    /// <param name="Accrued">amount of accrued bonuses</param>
    /// <param name="Withdrawn">amount of withdrawn bonuses	</param>
    /// <param name="Immediate">amount of immediate discount</param>
    /// <param name="Quantity">quantity of product</param>
    /// <param name="Price">price of product</param>
    public class ReceiptItem
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("accrued")]
        public float Accrued { get; set; }

        [JsonProperty("withdrawn")]
        public float Withdrawn { get; set; }

        [JsonProperty("immediate")]
        public float Immediate { get; set; }

        [JsonProperty("amount")]
        public float Quantity { get; set; }

        [JsonProperty("unit_price")]
        public float Price { get; set; }
    }

    /// <summary>Receipt response of discount and confirm methods</summary>
    /// <param name="Discount">amount of discount: withdrawn + immediate</param>
    /// <param name="Info">description of modules</param>
    /// <param name="Accrued">amount of accrued bonuses</param>
    /// <param name="Withdrawn">amount of withdrawn bonuses</param>
    /// <param name="Immediate">amount of immediate discount</param>
    /// <param name="UserName">client name</param>
    /// <param name="Items">list of products</param>
    /// <param name="AnalyticObjects">details of smartbonus modules</param>
    public class ReceiptResult
    {
        [JsonProperty("discount")]
        public float Discount { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("user_add_bonus")]
        public float Accrued { get; set; }

        [JsonProperty("withdrawn")]
        public float Withdrawn { get; set; }

        [JsonProperty("immediate")]
        public float Immediate { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("nomenclatures")]
        public List<ReceiptItem> Items { get; set; }

        [JsonProperty("analytics_object")]
        public AnalyticObject AnalyticObjects { get; set; }
    }

    /// <summary>Nomenclature item</summary>
    /// <param name="ID">your product identifier</param>
    /// <param name="Quantity">quantity or product</param>
    /// <param name="Price">price of product</param>
    public class NomenclatureItem
    {
        [JsonProperty("nomenclature_id")]
        public string ID { get; set; }

        [JsonProperty("amount")]
        public float Quantity { get; set; }

        [JsonProperty("unit_price")]
        public float Price { get; set; }

        public NomenclatureItem(string id, float quantity, float price)
        {
            ID = id;
            Quantity = quantity;
            Price = price;
        }
    }
}
