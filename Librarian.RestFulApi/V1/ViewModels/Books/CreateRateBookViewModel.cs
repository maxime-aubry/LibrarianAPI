namespace Librarian.RestFulAPI.V1.ViewModels.Books
{
    public class CreateRateBookViewModel
    {
        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public float Rate { get; set; }
        public string Commment { get; set; }
    }
}
