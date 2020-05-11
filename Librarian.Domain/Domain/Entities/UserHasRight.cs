using Librarian.Core.Domain.Enums;
using System;

namespace Librarian.Core.Domain.Entities
{
    public class UserHasRight
    {
        public UserHasRight()
        {

        }

        public UserHasRight(string id, string userId, EUserRight userRight, DateTime dateOfAdding, DateTime? dateOfEnding)
            : base()
        {
            this.Id = id;
            this.UserId = userId;
            this.UserRight = userRight;
            this.DateOfAdding = dateOfAdding;
            this.DateOfEnding = dateOfEnding;
        }

        public UserHasRight(string userId, EUserRight userRight, DateTime dateOfAdding, DateTime? dateOfEnding)
            : base()
        {
            this.Id = string.Empty;
            this.UserId = userId;
            this.UserRight = userRight;
            this.DateOfAdding = dateOfAdding;
            this.DateOfEnding = dateOfEnding;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public EUserRight UserRight { get; set; }
        public DateTime DateOfAdding { get; set; }
        public DateTime? DateOfEnding { get; set; }
    }
}
