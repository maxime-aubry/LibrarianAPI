using System;

namespace Librarian.Core.Domain.Entities
{
    public class Reader
    {
        public Reader()
        {

        }

        public Reader(string id, string firstName, string lastName, DateTime birthday, bool isForbidden)
            : base()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.IsForbidden = isForbidden;
        }

        public Reader(string firstName, string lastName, DateTime birthday, bool isForbidden)
            : base()
        {
            this.Id = string.Empty;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.IsForbidden = isForbidden;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsForbidden { get; set; }
    }
}
