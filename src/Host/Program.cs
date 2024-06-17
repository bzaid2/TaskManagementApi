using Asp.Versioning.Conventions;
using TaskManagement.Application;
using TaskManagement.Infrastructure;

string CORSOpenPolicy = "OpenCORSPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(
      name: CORSOpenPolicy,
      builder => {
          builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
      });
});

var app = builder.Build();

//Create the project versions
ApplicationVersion.VersionSet = app.NewApiVersionSet()
                                   .HasApiVersion(1, 0)
                                   .ReportApiVersions()
                                   .Build();

// Configure the HTTP request pipeline.
app.UseInfrastructure();
app.UseCors(CORSOpenPolicy);
app.Run();