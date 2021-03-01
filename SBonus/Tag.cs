using Newtonsoft.Json;

namespace SBonus
{
    /// <summary>
    ///     Tag is smartbonus filter, used in smartbonus app catalog
    /// </summary>
    public class Tag
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("group_id")]
        public string GroupID { get; set; }

        [JsonProperty("is_group")]
        public bool IsGroup { get; set; }

        public Tag(string id, string name, string groupID = null, bool isGroup = false)
        {
            ID = id;
            Name = name;
            GroupID = groupID;
            IsGroup = isGroup;
        }
    }
}
