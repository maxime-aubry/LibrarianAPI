namespace Librarian.Core.DataTransfertObject
{
    public sealed class Error
    {
        public Error(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; }
        public string Description { get; }
    }
}
