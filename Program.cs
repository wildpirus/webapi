using webapi;
using webapi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Configuration.GetConnectionString("dbEngine") == "SQLServer"){
    builder.Services.AddSqlServer<SupersContext>(builder.Configuration.GetConnectionString("supersSQLSR"));
}else if (builder.Configuration.GetConnectionString("dbEngine") == "PostgreSQL"){
    builder.Services.AddNpgsql<SupersContext>(builder.Configuration.GetConnectionString("supersPGSQL"));
}

//builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
//builder.Services.AddScoped<IHelloWorldService>(p=> new HelloWorldService());

builder.Services.AddScoped<ISupersService, SupersService>();
builder.Services.AddScoped<IRasgosSupersService, RasgosSupersService>();
builder.Services.AddScoped<IRasgosService, RasgosService>();
builder.Services.AddScoped<IPeleasService, PeleasService>();
builder.Services.AddScoped<IEnfrentamientosService, EnfrentamientosService>();
builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IPatrocinadoresService, PatrocinadoresService>();




builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseWelcomePage();

//app.UseTimeMiddleware();

app.MapControllers();

app.Run();