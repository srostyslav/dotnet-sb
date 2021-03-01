using System.Collections.Generic;
using Newtonsoft.Json;

namespace SBonus
{
    /// <summary>
    ///     Nomenclature instance has to be sync to smartbonus after it created, changed or deleted.
    ///     If you cannot trigger nomenclature events, send it by some interval: once a day for example.
    /// </summary>
    /// <param name="ID">unique identifier of product in your db</param>
    /// <param name="Name">title of product</param>
    /// <param name="Description">description of product (optional)</param>
    /// <param name="Image">image of product, if you have more than one image join them by comma (optional)</param>
    /// <param name="IsDeleted">send true if you deleted that product (optional)</param>
    /// <param name="CategoryID">unique identifier of category in your db (optional)</param>
    /// <param name="Barcode">barcode of your product (optional)</param>
    /// <param name="Price">price of your product (optional)</param>
    /// <param name="IsCategory">send true if current instance is category or false if it's product (optional)</param>
    /// <param name="Tags">list of tag identifiers (optional)</param>
    /// <param name="CanBuy">send true if this product can be buyed in smartbonus app (optional)</param>
    /// <param name="IsHidden">send false if you want to show this product in smartbonus app catalog for clients (optional)</param>
    public class Nomenclature
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("photo_url")] 
        public string Image { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [JsonProperty("category")]
        public string CategoryID { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; } = 0;

        [JsonProperty("is_category")]
        public bool IsCategory { get; set; } = false;

        [JsonProperty("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonProperty("can_buy")]
        public bool CanBuy { get; set; } = false;

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; } = true;
    }
}
