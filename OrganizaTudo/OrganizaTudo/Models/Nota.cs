using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrganizaTudo.Models
{
    public class Nota
    {
        [JsonProperty("_id")]
        public Id id { get; set; }

        [JsonProperty("data")]
        public Data ordem { get; set; }

        public string usuario { get; set; }
        public string titulo { get; set; }
        public string nota { get; set; }
        public bool publica { get; set; }
    }

    public partial class Id
    {
        [JsonProperty("$oid")]
        public string Oid { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("$numberLong")]
        public string data { get; set; }
    }

}
