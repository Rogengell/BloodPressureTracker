using EFramework.Data;
using Microsoft.EntityFrameworkCore;
using Patient.Service;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddDbContext<BPDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSingleton<FeatureHub>(provider => 
    var featureHub = new FeatureService();
    featureHub.Connect();
    return featureHub;
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

