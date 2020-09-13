using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        //[JsonProperty(PropertyName = "testresult")]
        //public bool TestResult { get; set; }

        [JsonProperty(PropertyName = "file")]
        public IFormFile File { get; set; }

    }
}
