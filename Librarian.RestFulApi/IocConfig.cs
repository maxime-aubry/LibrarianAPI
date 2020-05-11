﻿using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
using Librarian.Core.DataTransfertObject.GatewayResponses.Services;
using Librarian.Core.DataTransfertObject.UseCases.Authors;
using Librarian.Core.DataTransfertObject.UseCases.AuthorWritesBook;
using Librarian.Core.DataTransfertObject.UseCases.Books;
using Librarian.Core.DataTransfertObject.UseCases.ReaderLoansBook;
using Librarian.Core.DataTransfertObject.UseCases.ReaderRatesBook;
using Librarian.Core.DataTransfertObject.UseCases.Readers;
using Librarian.Core.DataTransfertObject.UseCases.Shelves;
using Librarian.Core.UseCases;
using Librarian.Core.UseCases.Authors;
using Librarian.Core.UseCases.AuthorWritesBook;
using Librarian.Core.UseCases.Books;
using Librarian.Core.UseCases.ReaderLoansBook;
using Librarian.Core.UseCases.ReaderRatesBook;
using Librarian.Core.UseCases.Readers;
using Librarian.Core.UseCases.Shelves;
using Librarian.Infrastructure.Mapper;
using Librarian.Infrastructure.MongoDBDataAccess;
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using Librarian.Infrastructure.MongoDBDataAccess.Repositories;
using Librarian.Infrastructure.Services.Auth;
using Librarian.RestFulAPI.Tools;
using Librarian.RestFulAPI.Tools.Presenters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

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

            services.AddScoped<IJwtService, JwtService>();
        }

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LibrarianDatabaseSettings>(configuration.GetSection(nameof(LibrarianDatabaseSettings)));
            services.AddSingleton<ILibrarianDatabaseSettings>(sp => sp.GetRequiredService<IOptions<LibrarianDatabaseSettings>>().Value);
            services.AddSingleton<IMongoDbContext, MongoDbContext>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorWritesBookRepository, AuthorWritesBookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IReaderLoansBookRepository, ReaderLoansBookRepository>();
            services.AddScoped<IReaderRatesBookRepository, ReaderRatesBookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserHasRightRepository, UserHasRightRepository>();

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
            services.AddScoped<Core.DataTransfertObject.UseCases.Authors.IDeleteAuthorUseCase, Core.UseCases.Authors.DeleteAuthorUseCase>();
            #endregion

            #region AuthorWritesBook
            services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
            services.AddScoped<Core.DataTransfertObject.UseCases.AuthorWritesBook.IDeleteAuthorUseCase, Core.UseCases.AuthorWritesBook.DeleteAuthorUseCase>();
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

            #region Users

            #endregion

            #region UserHasRight

            #endregion

            #region All use case providers
            services.AddScoped<IAuthorsUseCasesProvider, AuthorsUseCasesProvider>();
            services.AddScoped<IAuthorWritesBookUseCasesProvider, AuthorWritesBookUseCasesProvider>();
            services.AddScoped<IBooksUseCasesProvider, BooksUseCasesProvider>();
            services.AddScoped<IReadersUseCasesProvider, ReadersUseCasesProvider>();
            services.AddScoped<IReaderLoansBookUseCasesProvider, ReaderLoansBookUseCasesProvider>();
            services.AddScoped<IReaderRatesBookUseCasesProvider, ReaderRatesBookUseCasesProvider>();
            services.AddScoped<IShelvesUseCasesProvider, ShelvesUseCasesProvider>();
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

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IISOptions>(options => options.AutomaticAuthentication = false);
            services
                    .AddAuthentication(config =>
                    {
                        config.DefaultAuthenticateScheme = IISDefaults.AuthenticationScheme;
                        //config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                    {
                        config.RequireHttpsMetadata = false;
                        config.SaveToken = true;
                        config.TokenValidationParameters = new TokenValidationParameters
                        {
                            IssuerSigningKey = JwtService.GetSymmetricSecurityKey(configuration.GetSection("JwtConfig").GetSection("secret").Value),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromMinutes(5)
                        };
                    });
        }
    }
}