using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimeIsMoney.Application;
using TimeIsMoney.Repository.Infrastructure;
using TimeIsMoney.Telegram;

namespace TimeIsMoney;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        RegisterServices(services);
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite("Data Source=app.db"));

        var provider = services.BuildServiceProvider();
        using (var scope = provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.MigrateAsync();
        }
        
        var myService = provider.GetRequiredService<IMessageHandler>();
        using var cts = new CancellationTokenSource();
        var token = File.ReadAllText("token.txt");
        var bot = new TelegramBotClient(token, cancellationToken: cts.Token);
        var me = await bot.GetMe();
        bot.OnMessage += OnMessage;
        bot.OnError += OnError;

        Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
        Console.ReadLine();
        await cts.CancelAsync();
        return;


        async Task OnMessage(Message msg, UpdateType type)
        {
            var response = await myService.HandleAsync(msg, type);
            if (response != null)
            {
                await bot.SendMessage(msg.Chat, $"{response.RespondText}");
            }
        }

        Task OnError(Exception e, HandleErrorSource source)
        {
            Console.WriteLine($"{e}");
            return Task.CompletedTask;
        }
    }
    
    static void RegisterServices(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType)
            .ToList();

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();

            foreach (var iface in interfaces)
            {
                var implementations = types.Where(t => iface.IsAssignableFrom(t)).ToList();
                if (implementations.Count == 1)
                {
                    services.AddTransient(iface, type);
                }
            }
        }
    }

}