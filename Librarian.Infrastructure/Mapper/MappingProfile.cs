using AutoMapper;
using Librarian.Core.Domain.Enums;
using System;
using System.Linq;

namespace Librarian.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /* Domain > Infrastructure */
            CreateMap<Librarian.Core.Domain.Entities.Author, Librarian.Infrastructure.Entities.Author>().ConvertUsing(src => new Librarian.Infrastructure.Entities.Author(
                src.Id,
                src.FirstName,
                src.LastName
            ));
            CreateMap<Librarian.Core.Domain.Entities.AuthorWritesBook, Librarian.Infrastructure.Entities.AuthorWritesBook>().ConvertUsing(src => new Librarian.Infrastructure.Entities.AuthorWritesBook(
                src.Id,
                src.AuthorId,
                src.BookId
            ));
            CreateMap<Librarian.Core.Domain.Entities.Book, Librarian.Infrastructure.Entities.Book>().ConvertUsing(src => new Librarian.Infrastructure.Entities.Book(
                src.Id,
                src.Title,
                src.Categories.Select(c => (int)c),
                src.RealeaseDate,
                src.NumberOfCopies,
                src.ShelfId
            ));
            CreateMap<Librarian.Core.Domain.Entities.Reader, Librarian.Infrastructure.Entities.Reader>().ConvertUsing(src => new Librarian.Infrastructure.Entities.Reader(
                src.Id,
                src.FirstName,
                src.LastName,
                src.Birthday,
                src.IsForbidden
            ));
            CreateMap<Librarian.Core.Domain.Entities.ReaderLoansBook, Librarian.Infrastructure.Entities.ReaderLoansBook>().ConvertUsing(src => new Librarian.Infrastructure.Entities.ReaderLoansBook(
                src.Id,
                src.ReaderId,
                src.BookId,
                src.DateOfLoaning,
                src.EndDateOfLoaning,
                src.IsLost
            ));
            CreateMap<Librarian.Core.Domain.Entities.ReaderRatesBook, Librarian.Infrastructure.Entities.ReaderRatesBook>().ConvertUsing(src => new Librarian.Infrastructure.Entities.ReaderRatesBook(
                src.Id,
                src.ReaderId,
                src.BookId,
                src.Rate,
                src.Comment,
                src.DateOfRate
            ));
            CreateMap<Librarian.Core.Domain.Entities.Shelf, Librarian.Infrastructure.Entities.Shelf>().ConvertUsing(src => new Librarian.Infrastructure.Entities.Shelf(
                src.Id,
                src.Name,
                src.MaxQtyOfBooks,
                (int)src.Floor,
                (int)src.BookCategory
            ));

            /* Infrastructure > Domain */
            CreateMap<Librarian.Infrastructure.Entities.Author, Librarian.Core.Domain.Entities.Author>().ConvertUsing(src => new Librarian.Core.Domain.Entities.Author(
                src.Id,
                src.FirstName,
                src.LastName
            ));
            CreateMap<Librarian.Infrastructure.Entities.AuthorWritesBook, Librarian.Core.Domain.Entities.AuthorWritesBook>().ConvertUsing(src => new Librarian.Core.Domain.Entities.AuthorWritesBook(
                src.Id,
                src.AuthorId,
                src.BookId
            ));
            CreateMap<Librarian.Infrastructure.Entities.Book, Librarian.Core.Domain.Entities.Book>().ConvertUsing(src => new Librarian.Core.Domain.Entities.Book(
                src.Id,
                src.Title,
                src.Categories.Select(c => (EBookCategory)Enum.ToObject(typeof(EBookCategory), c)),
                src.RealeaseDate,
                src.NumberOfCopies,
                src.ShelfId
            ));
            CreateMap<Librarian.Infrastructure.Entities.Reader, Librarian.Core.Domain.Entities.Reader>().ConvertUsing(src => new Librarian.Core.Domain.Entities.Reader(
                src.Id,
                src.FirstName,
                src.LastName,
                src.Birthday,
                src.IsForbidden
            ));
            CreateMap<Librarian.Infrastructure.Entities.ReaderLoansBook, Librarian.Core.Domain.Entities.ReaderLoansBook>().ConvertUsing(src => new Librarian.Core.Domain.Entities.ReaderLoansBook(
                src.Id,
                src.ReaderId,
                src.BookId,
                src.DateOfLoaning,
                src.EndDateOfLoaning,
                src.IsLost
            ));
            CreateMap<Librarian.Infrastructure.Entities.ReaderRatesBook, Librarian.Core.Domain.Entities.ReaderRatesBook>().ConvertUsing(src => new Librarian.Core.Domain.Entities.ReaderRatesBook(
                src.Id,
                src.ReaderId,
                src.BookId,
                src.Rate,
                src.Comment,
                src.DateOfRate
            ));
            CreateMap<Librarian.Infrastructure.Entities.Shelf, Librarian.Core.Domain.Entities.Shelf>().ConvertUsing(src => new Librarian.Core.Domain.Entities.Shelf(
                src.Id,
                src.Name,
                src.MaxQtyOfBooks,
                (EFloor)Enum.ToObject(typeof(EFloor), src.Floor),
                (EBookCategory)Enum.ToObject(typeof(EBookCategory), src.BookCategory)
            ));
        }
    }
}
