// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Channels.Producer.Consumer.Core;
using Channels.Producer.Consumer.Core.Consumers;
using Channels.Producer.Consumer.Core.Consumers.Bikes;
using Channels.Producer.Consumer.Core.Consumers.Cars;
using Channels.Producer.Consumer.Core.Interfaces;
using Channels.Producer.Consumer.Core.Messages;
using System.Threading.Channels;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// create channels 
var carChannel =  Channel.CreateUnbounded<CarMessage>();
var bikeChannel = Channel.CreateUnbounded<BikeMessage>();

// inject message validators
builder.Services.AddTransient<IMessageValidator<BikeMessage>, BikeMessageValidator<BikeMessage>>();
builder.Services.AddTransient<IMessageValidator<CarMessage>, CarMessageValidator<CarMessage>>();

// inject consumers
builder.Services.AddTransient<BaseConsumer<CarMessage>>(x => new CarConsumer<CarMessage>(carChannel.Reader, new CarMessageValidator<CarMessage>()));
builder.Services.AddTransient<BaseConsumer<BikeMessage>>(x => new BikeConsumer<BikeMessage>(bikeChannel.Reader, new BikeMessageValidator<BikeMessage>()));

// inject producers
builder.Services.AddTransient<IProducer<CarMessage>>(x => new CarProducer(carChannel.Writer));
builder.Services.AddTransient<IProducer<BikeMessage>>(x => new BikeProducer(bikeChannel.Writer));


using IHost host = builder.Build();

await StartConsuming(host.Services);
await SendMessages(host.Services);


await host.RunAsync();

static Task StartConsuming(IServiceProvider serviceProvider)
{
    var carConsumer = GetService<BaseConsumer<CarMessage>>(serviceProvider);
    carConsumer.StartConsumingAsync();

    var bikeConsumer = GetService<BaseConsumer<BikeMessage>>(serviceProvider);
    bikeConsumer.StartConsumingAsync();

    return Task.CompletedTask;
}

static async Task SendMessages(IServiceProvider serviceProvider)
{
    var carProducer = GetService<IProducer<CarMessage>>(serviceProvider);    
    var bikeProducer = GetService<IProducer<BikeMessage>>(serviceProvider);

    var taskCar = SendCarMessages(carProducer);
    var taskBike = SendBikeMessages(bikeProducer);

    await Task.WhenAll(taskCar, taskBike);
}

static T GetService<T>(IServiceProvider serviceProvider)
{
    using IServiceScope serviceScope = serviceProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    return provider.GetRequiredService<T>();
}

static async Task SendCarMessages(IProducer<CarMessage> carProducer)
{
    var list = new List<CarMessage>
    {
        new CarMessage(){ Name = "Ferrari", Description = "Great speed!" },
        new CarMessage(){ Name = "Mercedes", Description = "Great performance!" },
        new CarMessage(){ Name = "Aston Martin", Description = "Great quality!" }
    };

    foreach (var message in list)
    {
        await carProducer.ProduceAsync(message);
    }
}

static async Task SendBikeMessages(IProducer<BikeMessage> bikeProducer)
{
    var list = new List<BikeMessage>
    {
        new BikeMessage(){ Name = "Shimano", Size = 18 },
        new BikeMessage(){ Name = "KTM", Size = 22 },
        new BikeMessage(){ Name = "Klm", Size = 14}
    };

    foreach (var message in list)
    {
        await bikeProducer.ProduceAsync(message);
    }
}
