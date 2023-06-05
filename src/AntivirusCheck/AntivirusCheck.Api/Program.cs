using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, config) =>
    {
        var address = Environment.GetEnvironmentVariable("RabbitMQ_Address");

        config.Host(new Uri(address), "TestConnection", host => {
            host.Username(Environment.GetEnvironmentVariable("RabbitMQ_User"));
            host.Password(Environment.GetEnvironmentVariable("RabbitMQ_Pass"));
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
