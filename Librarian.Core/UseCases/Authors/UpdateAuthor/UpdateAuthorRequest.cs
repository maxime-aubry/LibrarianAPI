﻿using HexagonalArchitecture.Core.DataTransfertObject;

namespace Librarian.Core.UseCases.Authors.UpdateAuthor
{
    public class UpdateAuthorRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateAuthorRequest(string authorId, string firstName, string lastName)
        {
            this.AuthorId = authorId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
