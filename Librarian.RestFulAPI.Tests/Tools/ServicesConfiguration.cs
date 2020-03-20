//using AutoMapper;
//using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
//using Librarian.Core.DataTransfertObject.UseCases.Authors;
//using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
//using Librarian.Core.DataTransfertObject.UseCases.Books;
//using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
//using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
//using Librarian.Core.DataTransfertObject.UseCases.Readers;
//using Librarian.Core.DataTransfertObject.UseCases.Shelves;
//using Librarian.Core.UseCases;
//using Librarian.Core.UseCases.Authors;
//using Librarian.Core.UseCases.AuthorWitesBook;
//using Librarian.Core.UseCases.Books;
//using Librarian.Core.UseCases.ReaderLoansBook;
//using Librarian.Core.UseCases.ReaderRatesBook;
//using Librarian.Core.UseCases.Readers;
//using Librarian.Core.UseCases.Shelves;
//using Librarian.Infrastructure.Mapper;
//using Librarian.Infrastructure.MongoDBDataAccess.Base;
//using Librarian.Infrastructure.MongoDBDataAccess.Repositories;
//using Librarian.RestFulAPI.Tools.Presenters;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using System.IO;

//namespace Librarian.RestFulAPI.Tests.Tools
//{
//    public static class ServicesConfiguration
//    {
//        public static void AddCustomServices(this IServiceCollection services)
//        {
//            IConfiguration configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
//                .AddEnvironmentVariables()
//                .Build();

//            // Auto Mapper
//            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
//            {
//                mc.AddProfile(new MappingProfile());
//                mc.AddProfile(new Librarian.Infrastructure.Mapper.MappingProfile());
//            });
//            services.AddSingleton(mappingConfig.CreateMapper());

//            // Database connection
//            services.Configure<LibrarianDatabaseSettings>(configuration.GetSection(nameof(LibrarianDatabaseSettings)));
//            services.AddSingleton<ILibrarianDatabaseSettings>(sp => sp.GetRequiredService<IOptions<LibrarianDatabaseSettings>>().Value);
//            services.AddSingleton<IMongoDbContext, MongoDbContext>();

//            // Repositories
//            services.AddScoped<IAuthorRepository, AuthorRepository>();
//            services.AddScoped<IAuthorWritesBookRepository, AuthorWritesBookRepository>();
//            services.AddScoped<IBookRepository, BookRepository>();
//            services.AddScoped<IReaderRepository, ReaderRepository>();
//            services.AddScoped<IReaderLoansBookRepository, ReaderLoansBookRepository>();
//            services.AddScoped<IReaderRatesBookRepository, ReaderRatesBookRepository>();
//            services.AddScoped<IShelfRepository, ShelfRepository>();

//            // Presenters
//            services.AddScoped(typeof(IJsonPresenter<>), typeof(JsonPresenter<>));

//            // Use Cases
//            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
//            services.AddScoped<IGetAuthorsUseCase, GetAuthorsUseCase>();
//            services.AddScoped<IGetAuthorsByFiltersUseCase, GetAuthorsByFiltersUseCase>();
//            services.AddScoped<ICreateAuthorUseCase, CreateAuthorUseCase>();
//            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
//            services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();

//            services.AddScoped<IAddAuthorsUseCase, AddAuthorsUseCase>();
//            services.AddScoped<IDeleteAuthorsUseCase, DeleteAuthorsUseCase>();
//            services.AddScoped<IGetAuthorsByBookIdUseCase, GetAuthorsByBookIdUseCase>();
//            services.AddScoped<IGetBooksByAuthorIdUseCase, GetBooksByAuthorIdUseCase>();

//            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
//            services.AddScoped<IGetBooksUseCase, GetBooksUseCase>();
//            services.AddScoped<IGetBooksByFiltersUseCase, GetBooksByFiltersUseCase>();
//            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
//            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
//            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();

//            services.AddScoped<IGetReaderByIdUseCase, GetReaderByIdUseCase>();
//            services.AddScoped<IGetReadersUseCase, GetReadersUseCase>();
//            services.AddScoped<ICreateReaderUseCase, CreateReaderUseCase>();
//            services.AddScoped<IUpdateReaderUseCase, UpdateReaderUseCase>();
//            services.AddScoped<IDeleteReaderUseCase, DeleteReaderUseCase>();

//            services.AddScoped<IAddLoanUseCase, AddLoanUseCase>();
//            services.AddScoped<ICloseLoanUseCase, CloseLoanUseCase>();
//            services.AddScoped<ICloseLoanAndDeclareAsLostUseCase, CloseLoanAndDeclareAsLostUseCase>();
//            services.AddScoped<IDeleteLoanUseCase, DeleteLoanUseCase>();

//            services.AddScoped<IAddRateUseCase, AddRateUseCase>();

//            services.AddScoped<IGetShelfByIdUseCase, GetShelfByIdUseCase>();
//            services.AddScoped<IGetShelvesUseCase, GetShelvesUseCase>();
//            services.AddScoped<ICreateShelfUseCase, CreateShelfUseCase>();
//            services.AddScoped<IUpdateShelfUseCase, UpdateShelfUseCase>();
//            services.AddScoped<IDeleteShelfUseCase, DeleteShelfUseCase>();
//            services.AddScoped<IGetAvailableShelvesUseCase, GetAvailableShelvesUseCase>();

//            // Use Cases Providers
//            services.AddScoped<IAuthorsUseCasesProvider, AuthorsUseCasesProvider>();
//            services.AddScoped<IAuthorWritesBookUseCasesProvider, AuthorWritesBookUseCasesProvider>();
//            services.AddScoped<IBooksUseCasesProvider, BooksUseCasesProvider>();
//            services.AddScoped<IReadersUseCasesProvider, ReadersUseCasesProvider>();
//            services.AddScoped<IReaderLoansBookUseCasesProvider, ReaderLoansBookUseCasesProvider>();
//            services.AddScoped<IReaderRatesBookUseCasesProvider, ReaderRatesBookUseCasesProvider>();
//            services.AddScoped<IShelvesUseCasesProvider, ShelvesUseCasesProvider>();
//            services.AddScoped<IUseCasesProvider, UseCasesProvider>();
//        }
//    }
//}
