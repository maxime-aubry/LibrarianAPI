using System;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class UpdateReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateReaderRequest(string readerId, string firstName, string lastName, DateTime birthday, bool isForbidden)
        {
            this.ReaderId = readerId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.IsForbidden = isForbidden;
        }

        public string ReaderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsForbidden { get; set; }
    }
}
