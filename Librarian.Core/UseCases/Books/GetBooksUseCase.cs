﻿using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Books
{
    public class GetBooksUseCase : UseCase, IGetBooksUseCase
    {
        public GetBooksUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetBooksRequest message, IOutputPort<UseCaseResponseMessage<IEnumerable<Book>>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Book>> books = await this.repositories.Books.Get();

                if (!books.Success)
                    throw new UseCaseException("Books not found", books.Errors);

                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(books.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<IEnumerable<Book>>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}