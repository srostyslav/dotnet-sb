using Newtonsoft.Json;

namespace SBonus
{
    /// <summary>Client instance is smartbonus app user</summary>
    /// <param name="Phone">phone number of client (unique)</param>
    /// <param name="Name">client name (optional)</param>
    /// <param name="Balance">amount of bonuses in smartbonus account</param>
    public class Client
    {
        [JsonProperty("phone")] 
        public string Phone { set; get; }

        [JsonProperty("name")] 
        public string Name { set; get; }

        [JsonProperty("balance")]
        public float Balance { set; get; }
    }
}
