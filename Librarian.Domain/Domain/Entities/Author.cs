namespace Librarian.Core.Domain.Entities
{
    public class Author
    {
        public Author()
        {

        }

        public Author(string id, string firstName, string lastName)
            : base()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Author(string firstName, string lastName)
            : base()
        {
            this.Id = string.Empty;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
