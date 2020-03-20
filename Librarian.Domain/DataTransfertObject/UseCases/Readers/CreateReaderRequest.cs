using System;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class CreateReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public CreateReaderRequest(string firstName, string lastName, DateTime birthday, bool isForbidden)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.IsForbidden = isForbidden;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsForbidden { get; set; }
    }
}
