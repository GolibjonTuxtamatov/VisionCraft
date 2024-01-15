using VisionCraft.Brokers.Loggings;
using VisionCraft.Brokers.OpenAIs;
using VisionCraft.Brokers.Pdfs;
using VisionCraft.Brokers.Storages;
using VisionCraft.Models.OpenAIs;
using VisionCraft.Services.Foundations.CVs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAIConfiguration>(builder.Configuration.GetSection("OpenAIConfiguration"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorageBroker>();

AddBrokers(builder);

builder.Services.AddTransient<ICVService, CVService>();

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
    builder.Services.AddTransient<IOpenAIBroker, OpenAIBroker>();
    builder.Services.AddTransient<IStorageBroker, StorageBroker>();
    builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
    builder.Services.AddTransient<IPdfBroker, PdfBroker>();
}