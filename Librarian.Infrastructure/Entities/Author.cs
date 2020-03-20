namespace Librarian.Infrastructure.Entities
{
    public class Author : BaseObject
    {
        public Author(string id, string firstName, string lastName)
            : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
