using System;

namespace Librarian.Core.Domain.Enums.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class EBookCategoryAttribute : Attribute
    {
        public bool IsFiction { get; set; }
    }
}
