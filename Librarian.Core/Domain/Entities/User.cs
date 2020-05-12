namespace Librarian.Core.Domain.Entities
{
    public class User
    {
        public User()
        {

        }

        public User(string id, string firstName, string lastName, string login)
            : base()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Login = login;
        }

        public User(string firstName, string lastName, string login)
            : base()
        {
            this.Id = string.Empty;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Login = login;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
    }
}
