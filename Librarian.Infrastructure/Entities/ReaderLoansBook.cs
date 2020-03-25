﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Librarian.Infrastructure.Entities
{
    public class ReaderLoansBook : BaseObject
    {
        public ReaderLoansBook(string id, string readerId, string bookId, DateTime dateOfLoaning, DateTime? endDateOfLoaning, bool? isLost)
            : base(id)
        {
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.DateOfLoaning = dateOfLoaning;
            this.EndDateOfLoaning = endDateOfLoaning;
            this.IsLost = isLost;
        }

        public string ReaderId { get; set; }
        public string BookId { get; set; }
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Utc, Representation = BsonType.Document)]
        public DateTime DateOfLoaning { get; set; }
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Utc, Representation = BsonType.Document)]
        public DateTime? EndDateOfLoaning { get; set; }
        public bool? IsLost { get; set; }
    }
}