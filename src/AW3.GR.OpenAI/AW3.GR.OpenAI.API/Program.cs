using AW3.GR.OpenAI.Application;
using AW3.GR.OpenAI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.
    Services.AddApplication()
            .AddInfrastructure();

builder.Services.AddControllers();
builder.Services.AddOpenAi(settings =>
{
    settings.ApiKey = builder.Configuration["OpenAI:ApiKey"];
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
