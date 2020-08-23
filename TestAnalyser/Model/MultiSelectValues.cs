using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAnalyser.Model
{
    public class MultiSelectValues
    {
        [JsonProperty("key")]
        public int key { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}