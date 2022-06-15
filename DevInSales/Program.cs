using DevInSales.Context;



using DevInSales.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<SqlContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

var secret = builder.Configuration.GetValue<string>("TokenConfigurations: SecretJwtKey");
var key = Encoding.UTF8.GetBytes("");

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});
.AddJwtBearer(o => 
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o => TokenValidationParameters = new TokenValidationParameters
    {
        ValidateSogningKey = new SymetricSecurityKey(key),
        ValidadeIssuer = false,
        ValidadeAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();