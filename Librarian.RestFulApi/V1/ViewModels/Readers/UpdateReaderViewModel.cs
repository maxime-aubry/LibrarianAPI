using System;

namespace Librarian.RestFulAPI.V1.ViewModels.Readers
{
    public class UpdateReaderViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
