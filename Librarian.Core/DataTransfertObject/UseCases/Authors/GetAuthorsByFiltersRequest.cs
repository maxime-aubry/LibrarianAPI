﻿using Librarian.Core.Domain.Entities;
using System.Collections.Generic;

namespace Librarian.Core.DataTransfertObject.UseCases.Authors
{
    public class GetAuthorsByFiltersRequest : IUseCaseRequest<UseCaseResponseMessage<IEnumerable<FindAuthorsByFilters>>>
    {
        public GetAuthorsByFiltersRequest(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}