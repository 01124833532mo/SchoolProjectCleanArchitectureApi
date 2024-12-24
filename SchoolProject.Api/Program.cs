
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using SchoolProject.Api.Services;
using SchoolProject.Core;
using SchoolProject.Infrastructure;
using SchoolProject.Service;
using System.Globalization;

namespace SchoolProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            builder.Services.AddInfrastructureDependencies();
            builder.Services.AddServicesDependencies();
            builder.Services.AddCoreDependencies();
            builder.Services.AddServiceRegistration(builder.Configuration);


            #region Localization
            builder.Services.AddControllersWithViews()
                     .AddViewLocalization(option =>
                     {
                         option.ResourcesPath = "";
                     });



            builder.Services.Configure<RequestLocalizationOptions>(options =>
                {
                    List<CultureInfo> supportedCultures = new List<CultureInfo>
                            {
                            new CultureInfo("en-US"),
                            new CultureInfo("de-DE"),
                            new CultureInfo("fr-FR"),
                            new CultureInfo("ar-EG")
                            };
                    options.DefaultRequestCulture = new RequestCulture("en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            //builder.Services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture =new RequestCulture("ar-EG");
            //});

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #region Localization MiddleWare
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            #endregion

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
