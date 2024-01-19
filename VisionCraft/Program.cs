using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OData;
using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.OpenAIs;
using VisionCraft.Brokers.Pdfs;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.OpenAIs;
using VisionCraft.Services.Foundations.CVs;
using VisionCraft.Services.Foundations.OpenAIs;
using VisionCraft.Services.Foundations.Pdfs;
using VisionCraft.Services.Foundations.Requirements;
using VisionCraft.Services.Foundations.Teams;
using VisionCraft.Services.Foundations.Vacancies;
using VisionCraft.Services.Orchestrations.CVOrchestrationService;
using VisionCraft.Services.Proccessings.CVs;
using VisionCraft.Services.Proccessings.OpenAIs;
using VisionCraft.Services.Proccessings.Pdfs;
using VisionCraft.Services.Proccessings.Requirements;
using VisionCraft.Services.Proccessings.Vacancies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAIConfiguration>(builder.Configuration.GetSection("OpenAIConfiguration"));

builder.Services.AddControllers().AddJsonOptions(options =>
                                                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                                .AddOData(options =>
                                                    options.Select().Filter().Expand().OrderBy().OrderBy().Count().SetMaxTop(50));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorageBroker>();

AddBrokers(builder);
AddServices(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

static void AddBrokers(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IPdfBroker, PdfBroker>();
    builder.Services.AddTransient<IOpenAIBroker, OpenAIBroker>();
    builder.Services.AddTransient<IStorageBroker, StorageBroker>();
    builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
}

static void AddServices(WebApplicationBuilder builder)
{
    // Foundation services
    builder.Services.AddTransient<ICVService, CVService>();
    builder.Services.AddTransient<IPdfService, PdfService>();
    builder.Services.AddTransient<ITeamService, TeamService>();
    builder.Services.AddTransient<IOpenAIService, OpenAIService>();
    builder.Services.AddTransient<IVacancyService, VacancyService>();
    builder.Services.AddTransient<IRequirementService, RequirementService>();

    // Proccesing services
    builder.Services.AddTransient<ICVProccessingService, CVProccessingService>();
    builder.Services.AddTransient<IPdfProccessingService, PdfProccessingService>();
    builder.Services.AddTransient<IOpenAIProccessingService, OpenAIProccessingService>();
    builder.Services.AddTransient<IVacancyProccessingService, VacancyProccessingService>();
    builder.Services.AddTransient<IRequirementProccessingService, RequirementProccessingService>();

    // Orchestration services
    builder.Services.AddTransient<ICVOrchestrationService, CVOrchestrationService>();
}