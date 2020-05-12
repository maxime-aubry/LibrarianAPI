using System;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class CreateReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateReaderRequest(string firstName, string lastName, DateTime birthday)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
