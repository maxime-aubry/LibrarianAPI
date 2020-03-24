using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Librarian.Infrastructure.Entities
{
    public class Reader : BaseObject
    {
        public Reader(string id, string firstName, string lastName, DateTime birthday, bool isForbidden)
            : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.IsForbidden = isForbidden;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Utc, Representation = BsonType.Document)]
        public DateTime Birthday { get; set; }
        public bool IsForbidden { get; set; }
    }
}
