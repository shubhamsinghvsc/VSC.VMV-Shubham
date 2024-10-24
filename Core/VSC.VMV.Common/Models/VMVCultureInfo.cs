using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.VMV.Common.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public class VMVCultureInfo
    {
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public string CultureName { get; set; } = "";

        [JsonProperty]
        public string CountryName { get; set; } = "";

        [JsonProperty]
        public string Currency { get; set; } = "";
    }
}
