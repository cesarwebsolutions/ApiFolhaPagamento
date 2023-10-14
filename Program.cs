
using ApiFolhaPagamento.AuthorizationTeste;
using ApiFolhaPagamento.Data;
using ApiFolhaPagamento.Models;
using ApiFolhaPagamento.Repositorios;
using ApiFolhaPagamento.Repositorios.Interfaces;
using ApiFolhaPagamento.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiFolhaPagamento
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IAuthorizationHandler, PermissaoAuthorization>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("CustomJwt",options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Secret())),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero

                };
            });

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaFolhaPagamentoDBContex>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            builder.Services.AddScoped<ILogin, LoginRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<CargoRepositorio>();
            builder.Services.AddScoped<ColaboradorRepositorio>();
            builder.Services.AddScoped<HoleriteRepositorio>();
            builder.Services.AddScoped<EmpresaRepositorio>();
            builder.Services.AddScoped<ILogin, LoginRepositorio>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Adm", policy => policy.AddRequirements(new Permissao(1)));
            });


            var chave = Encoding.ASCII.GetBytes(Settings.Secret());
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<SistemaFolhaPagamentoDBContex>();

                    // Verificar se o usuário 'admin' já existe no banco de dados
                    if (!dbContext.Usuarios.Any(u => u.Nome == "admin"))
                    {
                        // Se não existir, crie o usuário 'admin'
                        var adminUsuario = new UsuarioModel
                        {
                            Nome = "admin",
                            Email = "admin",
                            Senha = "admin",
                            PermissaoId = 1
                        };

                        dbContext.Usuarios.Add(adminUsuario);
                        await dbContext.SaveChangesAsync();
                        Console.WriteLine("Usuário 'admin' foi adicionado ao banco de dados.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro ao verificar/inserir o usuário 'admin': " + ex.Message);
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<SistemaFolhaPagamentoDBContex>();

                    // Verificar se a permissão 'ADMIN' já existe no banco de dados
                    if (!dbContext.Permissoes.Any(p => p.Permissao == "ADMIN"))
                    {
                        // Se não existir, crie a permissão 'ADMIN'
                        var permissaoAdmin = new Permissoes
                        {
                            Permissao = "ADMIN"
                        };

                        dbContext.Permissoes.Add(permissaoAdmin);
                    }

                    // Verificar se a permissão 'USER' já existe no banco de dados
                    if (!dbContext.Permissoes.Any(p => p.Permissao == "USER"))
                    {
                        // Se não existir, crie a permissão 'USER'
                        var permissaoUser = new Permissoes
                        {
                            Permissao = "USER"
                        };

                        dbContext.Permissoes.Add(permissaoUser);
                    }

                    await dbContext.SaveChangesAsync();
                    Console.WriteLine("Permissões 'ADMIN' e 'USER' foram adicionadas ao banco de dados.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro ao verificar/inserir as permissões: " + ex.Message);
                }
            }


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

            app.Run();
        }
    }
}