using webapi;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<SupersContext>(builder.Configuration.GetConnectionString("cnSupers"));
//builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
//builder.Services.AddScoped<IHelloWorldService>(p=> new HelloWorldService());
builder.Services.AddScoped<ISupersService, SupersService>();
builder.Services.AddScoped<IRasgosSupersService, RasgosSupersService>();
builder.Services.AddScoped<IRasgosService, RasgosService>();
builder.Services.AddScoped<IPeleasService, PeleasService>();
builder.Services.AddScoped<IEnfrentamientosService, EnfrentamientosService>();
builder.Services.AddScoped<IEventosService, EventosService>();
builder.Services.AddScoped<IPatrocinadoresService, PatrocinadoresService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseWelcomePage();

//app.UseTimeMiddleware();

app.MapControllers();

app.Run();