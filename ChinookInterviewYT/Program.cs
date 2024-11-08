using ChinookInterviewYT.Client.Pages;
using ChinookInterviewYT.Components;
using ChinookInterviewYT.Data;
using ChinookInterviewYT.Data.Repositories;
using ChinookInterviewYT.Data.Repositories.Interfaces;
using ChinookInterviewYT.Services;
using ChinookInterviewYT.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//add customer services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>();


builder.Services.AddDbContext<ChinookDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChinookDb"));
});

builder.Services.AddHttpClient();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Chinook API",
        Version = "v1",
        Description = "Describe APP API SPEC",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "SJ Wonder",
            Email = "Shelly@wonderwebdev.com",
        }
    });
});

builder.Services.AddControllers(); // enable API controllers

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    builder.Configuration.AddUserSecrets<Program>();
//normally you would use this in development only. But we want to expose the api as part of the challenge
app.UseSwagger();
app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapControllers(); // turn on routing for API controllers


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChinookInterviewYT.Client._Imports).Assembly);

app.Run();
