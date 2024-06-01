using ChecklistAPI.Models;
using ChecklistAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChecklistAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("SqlServerDb");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrador"));
                options.AddPolicy("RequireSindicoRole", policy => policy.RequireRole("Sindico"));
            });

            builder.Services.AddScoped<CondominioRepository>();
            builder.Services.AddScoped<TorreRepository>();
            builder.Services.AddScoped<RegistroRepository>();
            builder.Services.AddScoped<AdministradorRepository>();
            builder.Services.AddScoped<SindicoRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Inicializando roles e usuários
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                SeedRolesAndUsersAsync(roleManager, userManager).Wait();
            }

            app.Run();
        }

        private static async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (!await roleManager.RoleExistsAsync("Sindico"))
            {
                await roleManager.CreateAsync(new IdentityRole("Sindico"));
            }

            if (await userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com", Nome = "Admin Nome", Cpf = "123456789" };
                var result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrador");
                }
            }

            if (await userManager.FindByEmailAsync("sindico@sindico.com") == null)
            {
                var user = new ApplicationUser { UserName = "sindico@sindico.com", Email = "sindico@sindico.com", Nome = "Sindico Nome", Cpf = "987654321" };
                var result = await userManager.CreateAsync(user, "Sindico@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Sindico");
                }
            }
        }
    }
}
