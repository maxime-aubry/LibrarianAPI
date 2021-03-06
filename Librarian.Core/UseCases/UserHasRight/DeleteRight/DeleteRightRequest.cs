﻿using HexagonalArchitecture.Core.DataTransfertObject;
using Librarian.Core.Domain.Enums;

namespace Librarian.Core.UseCases.UserHasRight.DeleteRight
{
    public class DeleteRightRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public DeleteRightRequest(string userId, EUserRight userRight)
        {
            this.UserId = userId;
            this.UserRight = userRight;
        }

        public string UserId { get; set; }
        public EUserRight UserRight { get; set; }
    }
}
