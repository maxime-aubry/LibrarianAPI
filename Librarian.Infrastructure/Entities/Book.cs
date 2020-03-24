using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Librarian.Infrastructure.Entities
{
    public class Book : BaseObject
    {
        public Book(string id, string title, IEnumerable<int> categories, DateTime realeaseDate, int numberOfCopies, string shelfId)
            : base(id)
        {
            this.Title = title;
            this.Categories = categories;
            this.RealeaseDate = realeaseDate;
            this.NumberOfCopies = numberOfCopies;
            this.ShelfId = shelfId;
        }

        public string Title { get; set; }
        public IEnumerable<int> Categories { get; set; }
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Utc, Representation = BsonType.Document)]
        public DateTime RealeaseDate { get; set; }
        public int NumberOfCopies { get; set; }
        public string ShelfId { get; set; }
    }
}
