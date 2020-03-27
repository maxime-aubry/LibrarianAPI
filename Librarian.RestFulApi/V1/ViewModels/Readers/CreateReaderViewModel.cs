using System;

namespace Librarian.RestFulAPI.V1.ViewModels.Readers
{
    public class CreateReaderViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
