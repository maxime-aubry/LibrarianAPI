using System;

namespace Librarian.Infrastructure.MongoDBDataAccess.Base.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CollectionInfoAttribute : Attribute
    {
        public string CollectionName { get; set; }
    }
}
