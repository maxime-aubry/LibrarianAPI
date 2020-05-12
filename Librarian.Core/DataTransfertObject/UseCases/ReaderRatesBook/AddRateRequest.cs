namespace Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook
{
    public class AddRateRequest : IUseCaseRequest<UseCaseResponseMessage<string>>
    {
        public AddRateRequest(string readerId, string bookId, float rate, string comment)
        {
            this.ReaderId = readerId;
            this.BookId = bookId;
            this.Rate = rate;
            this.Comment = comment;
        }

        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public float Rate { get; set; }
        public string Comment { get; set; }
    }
}
