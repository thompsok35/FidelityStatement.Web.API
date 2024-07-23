using FidelityStatement.Web.API.Configuration;
using FidelityStatement.Web.API.DAL;
using FidelityStatement.Web.API.DAL.Contracts;
using FidelityStatement.Web.API.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("FidelityStatementDbConnectionString");
builder.Services.AddDbContext<FidelityStatementDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

//TODO: Change the default 'IdentityUser' to the extended user class 'Subscriber'
//builder.Services.AddDefaultIdentity<Subscriber>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStockRepository, StockRepository>();


builder.Services.AddAutoMapper(typeof(AutoMapperConfig));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Host.UseSerilog((logContext,logConfig) => logConfig.WriteTo.Console().ReadFrom.Configuration(logContext.Configuration));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
