using MentorShip.Models;
using MentorShip.Services;
using NServiceBus;
using Microsoft.OpenApi.Models;
using YamlDotNet.Serialization;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Host.UseNServiceBus(context =>
{
    // 
    var endpointConfiguration = new EndpointConfiguration("payment");
    endpointConfiguration.UseTransport<RabbitMQTransport>();
    //add license
    endpointConfiguration.LicensePath("NserviceBus\\License.xml");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    string connectionString = configuration.GetConnectionString("RabbitMQ");
    transport.ConnectionString(connectionString);
    //define endpoint 
    var routerConfig = transport.Routing();
    routerConfig.RouteToEndpoint(
        assembly: typeof(Message).Assembly,
        destination: "payment");

    //add custom message id field
    transport.CustomMessageIdStrategy(
        customIdStrategy: deliveryArgs =>
        {
            return Guid.NewGuid().ToString();
        });

    transport.UseDirectRoutingTopology(QueueType.Quorum);

    endpointConfiguration.SendOnly();

    return endpointConfiguration;
});

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ApplicationService>();
builder.Services.AddSingleton<CourseService>();
builder.Services.AddSingleton<SkillService>();
builder.Services.AddSingleton<MentorService>();
builder.Services.AddSingleton<PaymentService>();
builder.Services.AddSingleton<MenteeService>();
builder.Services.AddSingleton<PaymentService>();
builder.Services.AddSingleton<IConfiguration>(configuration);
// Add services to the container.

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Generate Swagger YAML file
    app.UseSwaggerUI(x => { x.SwaggerEndpoint("swagger/v1/swagger.yaml", "My API"); });
}


app.UseHttpsRedirection();

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
