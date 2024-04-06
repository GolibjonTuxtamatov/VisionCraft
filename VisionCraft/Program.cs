using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.OpenAIs;
using VisionCraft.Brokers.Pdfs;
using VisionCraft.Brokers.Storages;
using VisionCraft.Brokers.Tokens;
using VisionCraft.Models.OpenAIs;
using VisionCraft.Services.Foundations.CVs;
using VisionCraft.Services.Foundations.OpenAIs;
using VisionCraft.Services.Foundations.Pdfs;
using VisionCraft.Services.Foundations.Requirements;
using VisionCraft.Services.Foundations.Teams;
using VisionCraft.Services.Foundations.Tokens;
using VisionCraft.Services.Foundations.Vacancies;
using VisionCraft.Services.Orchestrations.CVOrchestrationService;
using VisionCraft.Services.Orchestrations.TeamOrchestrationServices;
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

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<StorageBroker>();

AddBrokers(builder);
AddServices(builder);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

static void AddBrokers(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IPdfBroker, PdfBroker>();
    builder.Services.AddTransient<IOpenAIBroker, OpenAIBroker>();
    builder.Services.AddTransient<IStorageBroker, StorageBroker>();
    builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
    builder.Services.AddTransient<ISecurityConfigurations, SecurityConfigurations>();
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
    builder.Services.AddTransient<ISecurityService, SecurityService>();

    // Proccesing services
    builder.Services.AddTransient<ICVProccessingService, CVProccessingService>();
    builder.Services.AddTransient<IPdfProccessingService, PdfProccessingService>();
    builder.Services.AddTransient<IOpenAIProccessingService, OpenAIProccessingService>();
    builder.Services.AddTransient<IVacancyProccessingService, VacancyProccessingService>();
    builder.Services.AddTransient<IRequirementProccessingService, RequirementProccessingService>();

    // Orchestration services
    builder.Services.AddTransient<ICVOrchestrationService, CVOrchestrationService>();
    builder.Services.AddTransient<ITeamOrchestrstionService, TeamOrchestrstionService>();
}