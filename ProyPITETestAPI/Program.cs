using AdminConta.AuthAPI.Helpers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Proyecto.PiteApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddEndpointsApiExplorer(); builder.Services.AddMvc()
     .AddNewtonsoftJson(
          options =>
          {
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
          });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Evaluación Peti",
        Description = "Web Api con Entity Framework Core en .Net 6 y Dapper",
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/V1/swagger.json", "Evaluación Peti");
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();