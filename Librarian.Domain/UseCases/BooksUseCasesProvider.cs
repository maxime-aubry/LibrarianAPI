﻿using Librarian.Core.DataTransfertObject.UseCases.Books;

namespace Librarian.Core.UseCases
{
    public class BooksUseCasesProvider : IBooksUseCasesProvider
    {
        public BooksUseCasesProvider(
            IGetBookByIdUseCase getById,
            IGetBooksUseCase getList,
            ICreateBookUseCase create,
            IUpdateBookUseCase update,
            IDeleteBookUseCase delete,
            IGetBooksByFiltersUseCase getBooksByFilters
        )
        {
            this.GetById = getById;
            this.GetList = getList;
            this.Create = create;
            this.Update = update;
            this.Delete = delete;
            this.GetBooksByFilters = getBooksByFilters;
        }

        public IGetBookByIdUseCase GetById { get; set; }

        public IGetBooksUseCase GetList { get; set; }

        public ICreateBookUseCase Create { get; set; }

        public IUpdateBookUseCase Update { get; set; }

        public IDeleteBookUseCase Delete { get; set; }
        public IGetBooksByFiltersUseCase GetBooksByFilters { get; set; }
    }
}