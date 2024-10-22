var builder = WebApplication.CreateBuilder(args);

var busSetting = builder.Configuration.GetSection(RabbitMqSettings.SettingPath).Get<RabbitMqSettings>()!;
builder.Services
    .AddDbContext<MainDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
    .AddSingleton<UserCreatedEventHandler>()
    .AddSingleton<IEmailService, EmailService>()
    .AddCap(x =>
    {
        x.UseEntityFramework<MainDbContext>();
        x.UseRabbitMQ(options =>
        {
            options.HostName = busSetting.Connection;
            options.UserName = busSetting.Username;
            options.Password = busSetting.Password;
        });
        x.DefaultGroupName = busSetting.QueueName;
        x.TopicNamePrefix = busSetting.TopicPrefix;
    });

var app = builder.Build();
app.Run();