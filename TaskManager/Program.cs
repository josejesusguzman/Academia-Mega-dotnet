using TaskManager.Repositories;
using TaskManager.Services;
using TaskManager.Shared.PubSub;
using TaskManager.Hubs;

var builder = WebApplication.CreateBuilder(args);

var AllowedOrigin = "BlazorClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowedOrigin, policy =>
    {
        policy.WithOrigins("http://localhost:5193")
            .AllowAnyHeader()
            .AllowAnyMethod();
            //.AllowCredentiads() Solo si usamois una cookie de sesión
    });
});

builder.Services.AddControllers();

builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<INotificationService, SmsNotificationService>();
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

builder.Services.AddSingleton<EventBus>();
builder.Services.AddSignalR();

//builder.Services.AddScoped<ITaskRepository, InMemoryTaskRepository>();



builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.MapHub<TaskEventHub>("/taskEvents");

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(AllowedOrigin);

app.UseHttpsRedirection();
app.Run();

