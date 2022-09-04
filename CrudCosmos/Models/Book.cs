using CrudCosmos.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CrudCosmos.Models
{
    public class Book : Entity
    {
        [JsonProperty(PropertyName = "title", Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }

        [JsonProperty(PropertyName = "authors", Required = Required.Always)]
        public IEnumerable<Author> Authors { get; set; }
    }

    public class Author
    {
        [JsonProperty(PropertyName = "firstName", Required = Required.Always)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}