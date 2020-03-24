using AutoMapper;
using Librarian.Core.DataTransfertObject.GatewayResponses.Repositories;
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
using Librarian.Infrastructure.MongoDBDataAccess.Base;
using Librarian.Infrastructure.MongoDBDataAccess.Repositories;
using Librarian.RestFulAPI.Tools;
using Librarian.RestFulAPI.Tools.Presenters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Librarian.RestFulAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    string origin = Configuration.GetSection("CustomSettings:Origin").Value;

                    builder.WithOrigins(origin)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddControllers();

            // Auto Mapper
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new Librarian.Infrastructure.Mapper.MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            // Database connection
            services.Configure<LibrarianDatabaseSettings>(Configuration.GetSection(nameof(LibrarianDatabaseSettings)));
            services.AddSingleton<ILibrarianDatabaseSettings>(sp => sp.GetRequiredService<IOptions<LibrarianDatabaseSettings>>().Value);
            services.AddSingleton<IMongoDbContext, MongoDbContext>();

            // Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorWritesBookRepository, AuthorWritesBookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
            services.AddScoped<IReaderLoansBookRepository, ReaderLoansBookRepository>();
            services.AddScoped<IReaderRatesBookRepository, ReaderRatesBookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();

            // Presenters
            services.AddScoped(typeof(IJsonPresenter<>), typeof(JsonPresenter<>));

            // Use Cases
            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            services.AddScoped<IGetAuthorsUseCase, GetAuthorsUseCase>();
            services.AddScoped<IGetAuthorsByFiltersUseCase, GetAuthorsByFiltersUseCase>();
            services.AddScoped<ICreateAuthorUseCase, CreateAuthorUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
            services.AddScoped<Core.DataTransfertObject.UseCases.Authors.IDeleteAuthorUseCase, Core.UseCases.Authors.DeleteAuthorUseCase>();

            services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
            services.AddScoped<Core.DataTransfertObject.UseCases.AuthorWritesBook.IDeleteAuthorUseCase, Core.UseCases.AuthorWritesBook.DeleteAuthorUseCase>();
            services.AddScoped<IGetAuthorsByBookIdUseCase, GetAuthorsByBookIdUseCase>();
            services.AddScoped<IGetBooksByAuthorIdUseCase, GetBooksByAuthorIdUseCase>();

            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            services.AddScoped<IGetBooksUseCase, GetBooksUseCase>();
            services.AddScoped<IGetBooksByFiltersUseCase, GetBooksByFiltersUseCase>();
            services.AddScoped<ICreateBookUseCase, CreateBookUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();

            services.AddScoped<IGetReaderByIdUseCase, GetReaderByIdUseCase>();
            services.AddScoped<IGetReadersUseCase, GetReadersUseCase>();
            services.AddScoped<ICreateReaderUseCase, CreateReaderUseCase>();
            services.AddScoped<IUpdateReaderUseCase, UpdateReaderUseCase>();
            services.AddScoped<IDeleteReaderUseCase, DeleteReaderUseCase>();

            services.AddScoped<IAddLoanUseCase, AddLoanUseCase>();
            services.AddScoped<ICloseLoanUseCase, CloseLoanUseCase>();
            services.AddScoped<ICloseLoanAndDeclareAsLostUseCase, CloseLoanAndDeclareAsLostUseCase>();
            services.AddScoped<IDeleteLoanUseCase, DeleteLoanUseCase>();

            services.AddScoped<IAddRateUseCase, AddRateUseCase>();

            services.AddScoped<IGetShelfByIdUseCase, GetShelfByIdUseCase>();
            services.AddScoped<IGetShelvesUseCase, GetShelvesUseCase>();
            services.AddScoped<ICreateShelfUseCase, CreateShelfUseCase>();
            services.AddScoped<IDeleteShelfUseCase, DeleteShelfUseCase>();
            services.AddScoped<IGetAvailableShelvesUseCase, GetAvailableShelvesUseCase>();

            // Use Cases Providers
            services.AddScoped<IAuthorsUseCasesProvider, AuthorsUseCasesProvider>();
            services.AddScoped<IAuthorWritesBookUseCasesProvider, AuthorWritesBookUseCasesProvider>();
            services.AddScoped<IBooksUseCasesProvider, BooksUseCasesProvider>();
            services.AddScoped<IReadersUseCasesProvider, ReadersUseCasesProvider>();
            services.AddScoped<IReaderLoansBookUseCasesProvider, ReaderLoansBookUseCasesProvider>();
            services.AddScoped<IReaderRatesBookUseCasesProvider, ReaderRatesBookUseCasesProvider>();
            services.AddScoped<IShelvesUseCasesProvider, ShelvesUseCasesProvider>();
            services.AddScoped<IUseCasesProvider, UseCasesProvider>();

            // Swagger
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider
           )
        {
            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Librarian API v{description.GroupName.ToUpperInvariant()}");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
