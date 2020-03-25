﻿namespace Librarian.Infrastructure.Entities
{
    public class AuthorWritesBook : BaseObject
    {
        public AuthorWritesBook(string id, string authorId, string bookId)
            : base(id)
        {
            this.AuthorId = authorId;
            this.BookId = bookId;
        }

        public string AuthorId { get; set; }
        public string BookId { get; set; }
    }
}