using Librarian.Core.Domain.Enums;

namespace Librarian.Core.Helpers
{
    public static class ShelfHelper
    {
        public static string GetShelfName(EFloor floor, EBookCategory bookCategory, int nbShelves)
        {
            string shelfName = $"F{(int)floor}-BC{(int)bookCategory}-NB{nbShelves}";
            return shelfName;
        }
    }
}
