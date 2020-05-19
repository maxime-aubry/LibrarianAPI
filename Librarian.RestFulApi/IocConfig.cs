using AutoMapper;
using HexagonalArchitecture.Core.Presenters;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.UseCases;
using Librarian.Core.UseCases.Authors;
using Librarian.Core.UseCases.Authors.CreateAuthor;
using Librarian.Core.UseCases.Authors.DeleteAuthor;
using Librarian.Core.UseCases.Authors.GetAuthorById;
using Librarian.Core.UseCases.Authors.GetAuthors;
using Librarian.Core.UseCases.Authors.GetAuthorsByFilters;
using Librarian.Core.UseCases.Authors.UpdateAuthor;
using Librarian.Core.UseCases.AuthorWritesBook;
using Librarian.Core.UseCases.AuthorWritesBook.AddAuthor;
using Librarian.Core.UseCases.AuthorWritesBook.GetAuthorsByBookId;
using Librarian.Core.UseCases.AuthorWritesBook.GetBooksByAuthorId;
using Librarian.Core.UseCases.Books;
using Librarian.Core.UseCases.Books.AddCopies;
using Librarian.Core.UseCases.Books.CreateBook;
using Librarian.Core.UseCases.Books.DeleteBook;
using Librarian.Core.UseCases.Books.GetBookById;
using Librarian.Core.UseCases.Books.GetBooks;
using Librarian.Core.UseCases.Books.GetBooksByFilters;
using Librarian.Core.UseCases.Books.RemoveCopies;
using Librarian.Core.UseCases.Books.UpdateBook;
using Librarian.Core.UseCases.ReaderLoansBook;
using Librarian.Core.UseCases.ReaderLoansBook.AddLoan;
using Librarian.Core.UseCases.ReaderLoansBook.CloseLoan;
using Librarian.Core.UseCases.ReaderLoansBook.CloseLoanAndDeclareAsLost;
using Librarian.Core.UseCases.ReaderLoansBook.DeleteLoan;
using Librarian.Core.UseCases.ReaderLoansBook.GetLoans;
using Librarian.Core.UseCases.ReaderRatesBook;
using Librarian.Core.UseCases.ReaderRatesBook.AddRate;
using Librarian.Core.UseCases.ReaderRatesBook.GetRates;
using Librarian.Core.UseCases.Readers;
using Librarian.Core.UseCases.Readers.CreateReader;
using Librarian.Core.UseCases.Readers.DeleteReader;
using Librarian.Core.UseCases.Readers.GetReaderById;
using Librarian.Core.UseCases.Readers.GetReaders;
using Librarian.Core.UseCases.Readers.UpdateReader;
using Librarian.Core.UseCases.Shelves;
using Librarian.Core.UseCases.Shelves.CreateShelf;
using Librarian.Core.UseCases.Shelves.DeleteShelf;
using Librarian.Core.UseCases.Shelves.GetAvailableShelves;
using Librarian.Core.UseCases.Shelves.GetShelfById;
using Librarian.Core.UseCases.Shelves.GetShelves;
using Librarian.Core.UseCases.UserHasRight;
using Librarian.Core.UseCases.UserHasRight.AddRight;
using Librarian.Core.UseCases.UserHasRight.DeleteRight;
using Librarian.Core.UseCases.Users;
using Librarian.Core.UseCases.Users.CreateUser;
using Librarian.Core.UseCases.Users.DeleteUser;
using Librarian.Core.UseCases.Users.GetUserById;
using Librarian.Core.UseCases.Users.GetUsers;
using Librarian.Core.UseCases.Users.UpdateUser;
using Librarian.Infrastructure.Mapper;
using Librarian.Infrastructure.MongoDBDataAccess;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using Librarian.Infrastructure.MongoDBDataAccess.Repositories;
using Librarian.RestFulAPI.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Librarian.RestFulAPI
{
    public static class IocConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new Librarian.Infrastructure.Mapper.MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());
        }

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LibrarianDatabaseSettings>(configuration.GetSection(nameof(LibrarianDatabaseSettings)));
            services.AddSingleton<ILibrarianDatabaseSettings>(sp => sp.GetRequiredService<IOptions<LibrarianDatabaseSettings>>().Value);
            //services.AddSingleton<IMongoDbContext, MongoDbContext>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorWritesBookRepository, AuthorWritesBookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IReaderLoansBookRepository, ReaderLoansBookRepository>();
            services.AddScoped<IReaderRatesBookRepository, ReaderRatesBookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();
            services.AddScoped<IUserHasRightRepository, UserHasRightRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRepositoryProvider, RepositoryProvider>();
        }

        public static void AddPresenters(this IServiceCollection services)
        {
            services.AddScoped(typeof(IJsonPresenter<>), typeof(JsonPresenter<>));
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            #region Authors
            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            services.AddScoped<IGetAuthorsUseCase, GetAuthorsUseCase>();
            services.AddScoped<IGetAuthorsByFiltersUseCase, GetAuthorsByFiltersUseCase>();
            services.AddScoped<ICreateAuthorUseCase, CreateAuthorUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
            services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
            #endregion

            #region AuthorWritesBook
            services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
            services.AddScoped<Librarian.Core.UseCases.AuthorWritesBook.DeleteAuthor.IDeleteAuthorUseCase, Librarian.Core.UseCases.AuthorWritesBook.DeleteAuthor.DeleteAuthorUseCase>();
            services.AddScoped<IGetAuthorsByBookIdUseCase, GetAuthorsByBookIdUseCase>();
            services.AddScoped<IGetBooksByAuthorIdUseCase, GetBooksByAuthorIdUseCase>();
            #endregion

            #region Books
            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            services.AddScoped<IGetBooksUseCase, GetBooksUseCase>();
            services.AddScoped<IGetBooksByFiltersUseCase, GetBooksByFiltersUseCase>();
            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            services.AddScoped<IAddCopiesUseCase, AddCopiesUseCase>();
            services.AddScoped<IRemoveCopiesUseCase, RemoveCopiesUseCase>();
            #endregion

            #region ReaderLoansBook
            services.AddScoped<IGetLoansUseCase, GetLoansUseCase>();
            services.AddScoped<IAddLoanUseCase, AddLoanUseCase>();
            services.AddScoped<ICloseLoanUseCase, CloseLoanUseCase>();
            services.AddScoped<ICloseLoanAndDeclareAsLostUseCase, CloseLoanAndDeclareAsLostUseCase>();
            services.AddScoped<IDeleteLoanUseCase, DeleteLoanUseCase>();
            #endregion

            #region ReaderRatesBook
            services.AddScoped<IGetRatesUseCase, GetRatesUseCase>();
            services.AddScoped<IAddRateUseCase, AddRateUseCase>();
            #endregion

            #region Readers
            services.AddScoped<IGetReaderByIdUseCase, GetReaderByIdUseCase>();
            services.AddScoped<IGetReadersUseCase, GetReadersUseCase>();
            services.AddScoped<ICreateReaderUseCase, CreateReaderUseCase>();
            services.AddScoped<IUpdateReaderUseCase, UpdateReaderUseCase>();
            services.AddScoped<IDeleteReaderUseCase, DeleteReaderUseCase>();
            #endregion

            #region Shelves
            services.AddScoped<IGetShelfByIdUseCase, GetShelfByIdUseCase>();
            services.AddScoped<IGetShelvesUseCase, GetShelvesUseCase>();
            services.AddScoped<ICreateShelfUseCase, CreateShelfUseCase>();
            services.AddScoped<IDeleteShelfUseCase, DeleteShelfUseCase>();
            services.AddScoped<IGetAvailableShelvesUseCase, GetAvailableShelvesUseCase>();
            #endregion

            #region UserHasRight
            services.AddScoped<IAddRightUseCase, AddRightUseCase>();
            services.AddScoped<IDeleteRightUseCase, DeleteRightUseCase>();
            #endregion

            #region Users
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
            services.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            #endregion

            #region All use case providers
            services.AddScoped<IAuthorsUseCasesProvider, AuthorsUseCasesProvider>();
            services.AddScoped<IAuthorWritesBookUseCasesProvider, AuthorWritesBookUseCasesProvider>();
            services.AddScoped<IBooksUseCasesProvider, BooksUseCasesProvider>();
            services.AddScoped<IReadersUseCasesProvider, ReadersUseCasesProvider>();
            services.AddScoped<IReaderLoansBookUseCasesProvider, ReaderLoansBookUseCasesProvider>();
            services.AddScoped<IReaderRatesBookUseCasesProvider, ReaderRatesBookUseCasesProvider>();
            services.AddScoped<IShelvesUseCasesProvider, ShelvesUseCasesProvider>();
            services.AddScoped<IUserHasRightUseCasesProvider, UserHasRightUseCasesProvider>();
            services.AddScoped<IUsersUseCasesProvider, UsersUseCasesProvider>();
            services.AddScoped<IUseCasesProvider, UseCasesProvider>();
            #endregion
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        //public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services
        //        .AddAuthentication(config =>
        //        {
        //            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })
        //        .AddJwtBearer(config =>
        //        {
        //            string secret = configuration["JwtToken:secret"];
        //            string issuer = configuration["JwtToken:issuer"];
        //            string audience = configuration["JwtToken:audience"];

        //            config.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                ValidateIssuerSigningKey = true,
        //                ValidIssuer = issuer,
        //                ValidAudience = audience,
        //                IssuerSigningKey = JwtService.GetSymmetricSecurityKey(secret)
        //            };

        //            //services.Configure<IISOptions>(options => options.AutomaticAuthentication = false);
        //            //services
        //            //        .AddAuthentication(config =>
        //            //        {
        //            //            config.DefaultAuthenticateScheme = IISDefaults.AuthenticationScheme;
        //            //            //config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            //            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //            //        })
        //            //        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
        //            //        {
        //            //            config.RequireHttpsMetadata = false;
        //            //            config.SaveToken = true;
        //            //            config.TokenValidationParameters = new TokenValidationParameters
        //            //            {
        //            //                IssuerSigningKey = JwtService.GetSymmetricSecurityKey(configuration.GetSection("JwtConfig").GetSection("secret").Value),
        //            //                ValidateIssuer = false,
        //            //                ValidateAudience = false,
        //            //                ValidateIssuerSigningKey = true,
        //            //                ValidateLifetime = true,
        //            //                ClockSkew = TimeSpan.FromMinutes(5)
        //            //            };
        //            //        });
        //        });
        //}
    }
}
