using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SmartLearning_salon.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ssn")]
        public string Ssn { get; set; }

        [JsonProperty(PropertyName = "test-result")]
        public string TestResult { get; set; }

    }
}
