using api.Middleware;
using Azure.Storage.Blobs;
using infrastructure;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.OpenApi.Models;
using Recipe_Web_App;
using service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IngredientRepository>();
builder.Services.AddSingleton<IngredientService>();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddSingleton<ReviewRepository>();
builder.Services.AddSingleton<ReviewService>();

builder.Services.AddSingleton<RecipeRepository>();
builder.Services.AddSingleton<RecipeService>();

builder.Services.AddSingleton<TagsRepository>();
builder.Services.AddSingleton<TagsService>();




    builder.Services.AddSingleton<BlobService>(provider =>
        {
            var connectionString = provider.GetService<IConfiguration>()!
                .GetConnectionString("AvatarStorage");
            var client = new BlobServiceClient(connectionString);
            return new BlobService(client);
        }
    );

builder.Services.AddSingleton<JWTTokenService>();
builder.Services.AddSingleton<TokenOptions>(services =>
{
    var configuration = services.GetRequiredService<IConfiguration>();
    var options = configuration.GetRequiredSection("JWT").Get<TokenOptions>()!;
    if (string.IsNullOrEmpty(options?.Address))
    {
        var server = services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
        options.Address = addresses?.FirstOrDefault();
    }

    return options;
});


builder.Services.AddSingleton<PasswordHashAlgorithm, Argon2IdPasswordHashAlgorithm>();
builder.Services.AddSingleton<PasswordRepository>();
builder.Services.AddSingleton<PasswordService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Makes Swagger work with tokens
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new string[]
            {
            }
        }
    });
});



var app = builder.Build();



// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(options =>
    {
        options.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
app.UseMiddleware<TokenBearerHandler>();
app.UseMiddleware<GlobalExceptionHandler>();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

