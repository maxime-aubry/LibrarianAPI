using System;

namespace Librarian.Core.DataTransfertObject.UseCases.Readers
{
    public class UpdateReaderRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public UpdateReaderRequest(string id, string firstName, string lastName, DateTime birthday, bool isForbidden)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
            this.IsForbidden = isForbidden;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsForbidden { get; set; }
    }
}
