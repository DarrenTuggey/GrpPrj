using System;
using GroupProject.Areas.Identity.Data;
using GroupProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QRCoder;
using Microsoft.Extensions.Azure;

namespace GroupProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds controllers with views of course!
            services.AddControllersWithViews();

            // Use SQL Database if in Azure, otherwise, use SQLite
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<GroupProjectAuthContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GroupProjectContextConnection")));
            else
                services.AddDbContext<GroupProjectAuthContext>(options =>
                    options.UseSqlite("Data Source=localdatabase.db"));

            // Automatically perform database migration
            //services.BuildServiceProvider().GetService<GroupProjectAuthContext>().Database.Migrate();
            
            // Cookie Monster Policy Omnomnom
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Service for the the Multi Factor Authentication QR Code Generator
            services.AddSingleton(new QRCodeService(new QRCodeGenerator()));

            // Used to create an admin account. Should not be enabled in production without being secured further.
            // services.AddSingleton<AdminRegistrationTokenService>();

            // Used to restrict some actions to admin users. It wasn't needed yet so there is no page where it is implemented. 
            services.AddAuthorization(options =>
                options.AddPolicy("Admin", policy =>
                    policy.RequireAuthenticatedUser()
                        .RequireClaim("IsAdmin", bool.TrueString)));

            // Anti Cross Site Request Forgery token
            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
            
            // Service gets the user and API key for the email sender
            services.Configure<AuthMessageSenderOptions>(Configuration);

            // Changed the token life span on the email validation / password requests from 24 hours to 3
            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3));

            // Implements the email service
            services.AddTransient<IEmailSender, EmailSender>();

            // Set Default Identity. Had to change the RequireConfirmedAccount to false because if you happen to register and do not
            // validate your email before logging out you will be unable to login again to request the validation email.
            services.AddDefaultIdentity<GroupProjectUser>(
                    options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<GroupProjectAuthContext>();
            
            // Creates the link between the storage account and the app service account. Storage account is used for the SignalR Chat Function
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration.GetConnectionString("AzureStorage"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Redirects Http to Https
            app.UseHttpsRedirection();

            // Enables static file serving to the request path
            app.UseStaticFiles();

            // Turn on Cookie Monster!
            app.UseCookiePolicy();

            // Adds endpoint routing middleware
            app.UseRouting();

            // Adds authentication
            app.UseAuthentication();

            // Adds authorization
            app.UseAuthorization();

            // Endpoints with the default set to Home/Index
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
