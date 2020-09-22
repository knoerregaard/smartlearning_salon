using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace SmartLearning_salon.Models
{
    public class PersonForm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Ssn { get; set; }
        public IFormFile File { get; set; }
    }

    public class Person
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ssn")]
        public string Ssn { get; set; }
    }

}
