using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TransferApi;
using TransferApi.Infrastructure.ExceptionHandling;
using TransferApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer API", Version = "v1" });
});
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
builder.Services.AddDbContext<TransferContext>(opt => opt.UseInMemoryDatabase("TransferList")
  .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
  .EnableSensitiveDataLogging()
);


// Add services to the container.




// builder.Services.AddSingleton<IClassroomService, ClassroomService>();
// builder.Services.AddSingleton<ILog, MyLogger>(); ;
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);

}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
