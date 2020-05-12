using Librarian.Core.Domain.Enums.Attributes;

namespace Librarian.Core.Domain.Enums
{
    public enum EBookCategory
    {
        Default,

        // Under Nonfiction Category
        [EBookCategory(IsFiction = false)]
        BiographyAndAutobiography,

        [EBookCategory(IsFiction = false)]
        Essay,

        [EBookCategory(IsFiction = false)]
        Memoir,

        [EBookCategory(IsFiction = false)]
        NarrativeNonfiction,

        [EBookCategory(IsFiction = false)]
        Periodicals,

        [EBookCategory(IsFiction = false)]
        ReferenceBooks,

        [EBookCategory(IsFiction = false)]
        SelfhelpBook,

        [EBookCategory(IsFiction = false)]
        Speech,

        [EBookCategory(IsFiction = false)]
        Textbook,

        [EBookCategory(IsFiction = false)]
        Poetry,

        // Under Fiction Category
        [EBookCategory(IsFiction = true)]
        ActionAndAdventure,

        [EBookCategory(IsFiction = true)]
        Anthology,

        [EBookCategory(IsFiction = true)]
        Classic,

        [EBookCategory(IsFiction = true)]
        ComicAndGraphicNovel,

        [EBookCategory(IsFiction = true)]
        CrimeAndDetective,

        [EBookCategory(IsFiction = true)]
        Drama,

        [EBookCategory(IsFiction = true)]
        Fable,

        [EBookCategory(IsFiction = true)]
        FairyTale,

        [EBookCategory(IsFiction = true)]
        FanFiction,

        [EBookCategory(IsFiction = true)]
        Fantasy,

        [EBookCategory(IsFiction = true)]
        HistoricalFiction,

        [EBookCategory(IsFiction = true)]
        Horror,

        [EBookCategory(IsFiction = true)]
        Humor,

        [EBookCategory(IsFiction = true)]
        Legend,

        [EBookCategory(IsFiction = true)]
        MagicalRealism,

        [EBookCategory(IsFiction = true)]
        Mystery,

        [EBookCategory(IsFiction = true)]
        Mythology,

        [EBookCategory(IsFiction = true)]
        RealisticFiction,

        [EBookCategory(IsFiction = true)]
        Romance,

        [EBookCategory(IsFiction = true)]
        Satire,

        [EBookCategory(IsFiction = true)]
        ScienceFiction,

        [EBookCategory(IsFiction = true)]
        ShortAndStory,

        [EBookCategory(IsFiction = true)]
        SuspenseAndThriller
    }
}
