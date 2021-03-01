using Newtonsoft.Json;

namespace SBonus
{
    /// <summary>
    ///     Item response of refund receipt request
    ///     rest = price * quantity - withdrawn - immediate
    /// </summary>
    /// <param name="ID">your product identifier</param>
    /// <param name="Immediate">amount of immediate discount</param>
    /// <param name="Withdrawn">amount of withdrawn bonuses</param>
    /// <param name="Accrued">amount of accrued bonuses</param>
    public class RefundItemResult
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("immediate")]
        public float Immediate { get; set; }

        [JsonProperty("withdrawn")]
        public float Withdrawn { get; set; }

        [JsonProperty("accrued")]
        public float Accrued { get; set; }
    }

    /// <summary>Item of receipt refund request</summary>
    /// <param name="ID">your product identifier</param>
    /// <param name="Quantity">quantity of product</param>
    public class RefundItem
    {
        [JsonProperty("nomenclature_id")]
        public string ID { get; set; }

        [JsonProperty("amount")]
        public float Quantity { get; set; }

        public RefundItem(string id, float quantity)
        {
            ID = id;
            Quantity = quantity;
        }
    }
}
