using API.Extension;
using Application.Mappings;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

builder.Services.ConfigureContext(configuration);
builder.Services.AddControllers();
builder.Services.RetryExtension(configuration);
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddPolicy("cors", builder =>
{
    builder
    .WithOrigins("http://localhost:4200", "https://ftusa-web.dev2.addinn-group.com")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureSwagger();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.
app.UseCors("cors");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
