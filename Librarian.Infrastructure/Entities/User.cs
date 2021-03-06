﻿using Librarian.Infrastructure.MongoDBDataAccess.Base.Attributes;

namespace Librarian.Infrastructure.Entities
{
    [CollectionInfo(CollectionName = "Users")]
    public class User : BaseObject
    {
        public User(string id, string firstName, string lastName, string login)
            : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Login = login;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
    }
}
