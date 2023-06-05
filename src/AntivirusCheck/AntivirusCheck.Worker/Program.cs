using AntivirusCheck.Worker.Consumers;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        services.AddMassTransit(config =>
        {
            config.AddConsumer<FileToScanConsumer>();
            config.UsingRabbitMq((context, rabbitConfig) =>
            {
                var address = Environment.GetEnvironmentVariable("RabbitMQ_Address");

                rabbitConfig.Host(new Uri(address), "TestConnection", host => {
                    host.Username(Environment.GetEnvironmentVariable("RabbitMQ_User"));
                    host.Password(Environment.GetEnvironmentVariable("RabbitMQ_Pass"));
                });

                rabbitConfig.ConfigureEndpoints(context);
            });
        });


    })
    .Build();

host.Run();
