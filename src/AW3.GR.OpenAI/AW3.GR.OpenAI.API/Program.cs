using AW3.GR.OpenAI.API;
using AW3.GR.OpenAI.API.Common.Middleware;
using AW3.GR.OpenAI.Application;
using AW3.GR.OpenAI.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthentication()
   .UseAuthorization();

app.MapControllers();

app.Run();