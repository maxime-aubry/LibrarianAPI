using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Librarian.Infrastructure.Entities
{
    public class UserHasRight : BaseObject
    {
        public UserHasRight(string id, string userId, int rightId, DateTime dateOfAdding, DateTime? dateOfEnding = null)
            : base(id)
        {
            this.UserId = userId;
            this.RightId = rightId;
            this.DateOfAdding = dateOfAdding;
            this.DateOfEnding = dateOfEnding;
        }

        public string UserId { get; set; }
        public int RightId { get; set; }
        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Utc, Representation = BsonType.Document)]
        public DateTime DateOfAdding { get; set; }
        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Utc, Representation = BsonType.Document)]
        public DateTime? DateOfEnding { get; set; }
    }
}
