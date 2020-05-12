namespace Librarian.Core.Domain.Entities
{
    public class FindAuthorsByFilters
    {
        public FindAuthorsByFilters()
        {

        }

        public FindAuthorsByFilters(string id, string firstName, string lastName, float pertinence, int numberOfViews)
            : base()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Pertinence = pertinence;
            this.NumberOfViews = numberOfViews;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Pertinence { get; set; }
        public int NumberOfViews { get; set; }
    }
}
