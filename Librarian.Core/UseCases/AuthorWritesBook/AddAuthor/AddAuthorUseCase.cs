﻿using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.AuthorWritesBook.AddAuthor
{
    public class AddAuthorUseCase : UseCase, IAddAuthorUseCase
    {
        public AddAuthorUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(AddAuthorRequest message, IOutputPort<UseCaseResponseMessage<string>> outputPort)
        {
            try
            {
                GateawayResponse<IEnumerable<Librarian.Core.Domain.Entities.AuthorWritesBook>> properties = await this.repositories.AuthorWritesBooks.Get();

                if (!properties.Success)
                    throw new UseCaseException("Properties not found", properties.Errors);

                // delete author from this book
                Librarian.Core.Domain.Entities.AuthorWritesBook property = (from awb in properties.Data
                                                                            where awb.BookId == message.BookId
                                                                            && awb.AuthorId == message.AuthorId
                                                                            select awb).SingleOrDefault();
                if (property != null)
                    throw new UseCaseException("Author is already linked to this book", null);

                // link author to this book
                Librarian.Core.Domain.Entities.AuthorWritesBook authorWritesBook = new Librarian.Core.Domain.Entities.AuthorWritesBook(message.AuthorId, message.BookId);
                GateawayResponse<string> addedProperty = await this.repositories.AuthorWritesBooks.Add(authorWritesBook);

                if (!addedProperty.Success)
                    throw new UseCaseException("Book not linked to this book", addedProperty.Errors);

                outputPort.Handle(new UseCaseResponseMessage<string>(addedProperty.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<string>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
