using Azure.Storage.Blobs;
using infrastructure;
using infrastructure.Repositories;
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


builder.Services.AddSingleton<PasswordHashAlgorithm, Argon2IdPasswordHashAlgorithm>();
builder.Services.AddSingleton<PasswordRepository>();
builder.Services.AddSingleton<PasswordService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
