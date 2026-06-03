using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using MvcZapatillasExamen.Data;
using MvcZapatillasExamen.Helpers;
using MvcZapatillasExamen.Repositories;
using MvcZapatillasExamen.Services;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceStorageS3>();

string secretName = builder.Configuration.GetValue<string>("Secretos:MySqlSecretName");
string region = builder.Configuration.GetValue<string>("AWS:Region");

string secretJson = HelperSecretManager.GetSecret(secretName, region);

string connectionString = "";
using (JsonDocument doc = JsonDocument.Parse(secretJson))
{
    connectionString = doc.RootElement.GetProperty("MySqlDb").GetString();
}

builder.Services.AddDbContext<ZapatillasContext>(options =>
    options.UseMySQL(connectionString));


builder.Services.AddScoped<RepositoryZapatillas>();
builder.Services.AddScoped<ZapatillasService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
