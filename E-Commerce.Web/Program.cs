using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;

using E_Commerce.Web.CustomMiddleWares;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
            });
            #endregion

            var app = builder.Build();

            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();

            #region Configure the HTTP request pipeline
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
