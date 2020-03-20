using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Librarian.Infrastructure.Entities
{
    public class BaseObject
    {
        public BaseObject(string id)
        {
            this.Id = id;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
