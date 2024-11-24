using BpmnEngine.Application.Processors;
using BpmnEngine.Camunda;
using BpmnEngine.Camunda.Extensions;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Communication;
using BpmnEngine.Services.Handlers;
using BpmnEngine.Services.Processes;
using BpmnEngine.Storage;
using BpmnEngine.Storage.Abstractions;
using BpmnEngine.Storage.Entities;
using BpmnEngine.Storage.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<HtmlHelperOptions>(o => o.ClientValidationEnabled = true);
builder.Services.AddTransient<IViewModelProvider, ViewModelProvider>();
builder.Services.AddTransient<IViewModelProcessor, ViewModelProcessor>();
builder.Services.AddTransient<IBusinessKeyGenerator, BusinessKeyGenerator>();

builder.Services.AddTransient<IDecisionService, DecisionService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IProcessRequestHandlingService, ProcessRequestHandlingService>();

builder.Services
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
    })
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.LoginPath = new PathString("/");
        options.LogoutPath = new PathString("/logout");
        options.AccessDeniedPath = new PathString("/");
        options.SlidingExpiration = true;
    });

builder.Services.AddIdentityCore<UserEntity>()
    .AddUserStore<UserStore>()
    .AddSignInManager();

var configuration = builder.Configuration;

var engineRestUri = configuration.GetRequiredSection(CamundaConstants.EngineRestAddress).Value;

var options = configuration.GetSection(nameof(SmtpOptions)).Get<SmtpOptions>();

builder.Services.AddSingleton(options);

builder.Services.AddTransient<IFormsRepository>(_ => new FormsRepository(
    configuration.GetConnectionString(StorageConstants.ConnectionStringName)));

builder.Services.AddTransient<IUsersRepository>(_ => new UsersRepository(
    configuration.GetConnectionString(StorageConstants.ConnectionStringName)));

builder.Services.AddTransient<IUserActionsRepository>(_ => new UserActionsRepository(
    configuration.GetConnectionString(StorageConstants.ConnectionStringName)));

builder.Services.AddTaskClient(client => client.BaseAddress = new Uri(engineRestUri));
builder.Services.AddProcessClient(client => client.BaseAddress = new Uri(engineRestUri));
builder.Services.AddMessageClient(client => client.BaseAddress = new Uri(engineRestUri));

builder.Services.AddCamundaWorker(CamundaConstants.WorkerName)
    .AddHandler<ManagerChecksHandler>()
    .AddHandler<BouDirectorChecksHandler>()
    .AddHandler<BouVerificationHandler>()
    .AddHandler<DirectorChecksHandler>()
    .AddHandler<InformSenderAcceptedHandler>()
    .AddHandler<InformSenderRejectedHandler>()
    .ConfigurePipeline(pipeline =>
    {
        pipeline.Use(next => async context =>
        {
            var logger = context.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Task {Id} has started", context.Task.Id);
            await next(context);
            logger.LogInformation("Task {Id} has ended", context.Task.Id);
        });
    });

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHealthChecks("/health");

app.Run();
