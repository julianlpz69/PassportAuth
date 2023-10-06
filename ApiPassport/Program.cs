using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication()
/* .AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
}) */
.AddTwitter(twitterOptions =>{
    twitterOptions.ConsumerKey = configuration["TwitterAuthSettings:ApiKey"];
    twitterOptions.ConsumerSecret = configuration["TwitterAuthSettings:ApiSecret"];
});

builder.Services.AddDbContext<ApiPassportContext>(options=>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "twitter",
    pattern: "account/twitterlogin",
    defaults: new { controller = "Account", action = "TwitterLogin" });

app.MapControllerRoute(
name: "twitter-callback",
pattern: "account/twittercallback",
defaults: new { controller = "Account", action = "TwitterCallback" });


app.Run();
