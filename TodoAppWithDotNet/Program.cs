using Microsoft.EntityFrameworkCore;
using TodoAppWithDotNet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

    //options.AddPolicy("AllowReactApp", policyBuilder =>
    //{
    //    policyBuilder.WithOrigins("https://todoappdotnetfrontend-dbeadmdggvh8g9g5.canadacentral-01.azurewebsites.net") // Allow only React app on port 3000
    //                 .AllowAnyMethod()
    //                 .AllowAnyHeader()
    //                 .AllowCredentials();
    //});





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework and SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

builder.Services.AddCors();
var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("https://todoappdotnetfrontend-dbeadmdggvh8g9g5.canadacentral-01.azurewebsites.net")
    .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();



app.UseAuthorization();

app.MapControllers();

app.Run();
