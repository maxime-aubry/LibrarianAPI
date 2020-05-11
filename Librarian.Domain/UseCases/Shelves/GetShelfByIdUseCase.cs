﻿using Librarian.Core.DataTransfertObject;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.Domain.Entities;
using System.Threading.Tasks;

namespace Librarian.Core.UseCases.Shelves
{
    public class GetShelfByIdUseCase : UseCase, IGetShelfByIdUseCase
    {
        public GetShelfByIdUseCase(IRepositoryProvider repositories)
            : base(repositories)
        {
        }

        public async Task<bool> Handle(GetShelfByIdRequest message, IOutputPort<UseCaseResponseMessage<Shelf>> outputPort)
        {
            try
            {
                GateawayResponse<Shelf> shelf = await this.repositories.Shelves.Get(message.ShelfId);

                if (!shelf.Success)
                    throw new UseCaseException("Shelf not found", shelf.Errors);

                outputPort.Handle(new UseCaseResponseMessage<Shelf>(shelf.Data, true));
                return true;
            }
            catch (UseCaseException e)
            {
                outputPort.Handle(new UseCaseResponseMessage<Shelf>(null, false, e.Message, e.Errors));
            }

            return false;
        }
    }
}
