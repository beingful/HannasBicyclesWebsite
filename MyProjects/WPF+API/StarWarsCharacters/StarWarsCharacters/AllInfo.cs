using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarWarsCharacters
{
    class AllInfo
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Prev { get; set; }

        [JsonProperty("results")]
        public List<Character> characters { get; set; }
    }
}
