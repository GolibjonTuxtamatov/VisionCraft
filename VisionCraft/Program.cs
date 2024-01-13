using VisionCraft.Brokers.OpenAIs;
using VisionCraft.Models.OpenAIs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAIConfiguration>(builder.Configuration.GetSection("OpenAIConfiguration"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
