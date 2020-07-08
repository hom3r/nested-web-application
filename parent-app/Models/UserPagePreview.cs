using System;
namespace parent.Models
{
    public class UserPagePreview
    {
        public UserPagePreview()
        {
        }

        public int ID { get; set; }

        public string Hash { get; set; }

        public bool Invalidated { get; set; }

        public int PageID { get; set; }

        public UserPage Page { get; set; }

        public DateTime Expiration { get; set; }
    }
}
