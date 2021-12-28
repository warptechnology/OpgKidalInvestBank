using System.Net;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;

namespace Web.App
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            WebHostEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Чтобы кирилические символы не переводились в соответствующий Unicode Hex Character Code
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            //services.AddBankIServiceCollection(saradjevaOptions =>
            //{
            //    Configuration.GetSection("BankOptions").Bind(saradjevaOptions);
            //});
            //services.AddAuthManagement(authOptions =>
            //{
            //    Configuration.GetSection("AuthOptions").Bind(authOptions);
            //});
            //services.AddAuth(Configuration);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
            });

            //services.AddAutoMapperSingleton(cfg => cfg.AddProfile<SaradjevaMapperProfile>());

            //services.Configure(options =>
            //    options.FileProviders.Add(new PhysicalFileProvider(Path.Combine(HostingEnvironment.ContentRootPath, "..\NAMEOFRCL")))
            //);
            //services.AddControllersWithViews().AddRazorRuntimeCompilation(options => options.FileProviders.Add(new PhysicalFileProvider(appDirectory)));
            //if (WebHostEnvironment.IsDevelopment())
            {
                services.AddControllersWithViews(); //.AddRazorRuntimeCompilation();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseRouting();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapGet("/", async context =>
        //        {
        //            await context.Response.WriteAsync("Hello World!");
        //        });
        //    });
        //}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles()
                .UseAuthentication()
                .UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }
    }
}
