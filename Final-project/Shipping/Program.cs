using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shipping.core.Interfaces;
using Shipping.core.Models;
using Shipping.Mapping;
using Shipping.Middleware;
using Shipping.repo.Implementation;
using Shipping.repo.ShippingCon;
using Shipping.services;
using System.Security.Claims;
using System.Text;

namespace Shipping
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ShippingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies();
            });
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.User.AllowedUserNameCharacters = string.Empty;  
                    options.User.RequireUniqueEmail = true;  
                })
                .AddEntityFrameworkStores<ShippingContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGovernorateService, GovernorateService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAutoMapper(typeof(RecjectProfile));
            builder.Services.AddScoped<ISpecialPrice, SpecialShippingPrice>();
            builder.Services.AddScoped<IGroupPermissionRepo, GroupPermissionRepo>();
            builder.Services.AddScoped<GroupService>();


            // Allow all origins (for development)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            //Add JWT
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
            if (key.Length < 32)
            {
                throw new Exception("JWT Secret Key must be at least 32 characters long.");
            }

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero 
                };
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            //  seed data
            using (var scope = app.Services.CreateScope())
            {
 
                await SeedDataAsync(scope.ServiceProvider); 
            }

            app.Run();
        }

        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUsername = "SuperAdmin";
            var existingAdmin = await userManager.FindByNameAsync(adminUsername);

            if (existingAdmin == null)
            {
                var admin = new Employee
                {
                    UserName = adminUsername,
                    NormalizedUserName = adminUsername.ToUpper(),
                    Email = "fares@shipping.com",
                    NormalizedEmail = "FARES@SHIPPING.COM",
                    Address = "Sers",
                    PhoneNumber = "01276227094",
                    Name = "Fares",
                    BranchId = 1,
                    GroupId = 1
                };

                var password = "Admin@123";
                admin.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(admin, password);

                var result = await userManager.CreateAsync(admin);
                if (result.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, admin.Id),
                        new Claim(ClaimTypes.Email, admin.Email),
                        new Claim(ClaimTypes.Name, admin.Name),
                        new Claim(ClaimTypes.Role, "Employee")
                    };

                    await userManager.AddClaimsAsync(admin, claims);
                }
            }
        }
    }
}
