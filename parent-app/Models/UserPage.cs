using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace parent.Models
{
    public class UserPage
    {
        public UserPage()
        {
        }


        public int ID { get; set; }

        [JsonIgnore]
        public IdentityUser Owner { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
