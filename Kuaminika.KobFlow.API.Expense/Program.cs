var builder = WebApplication.CreateBuilder(args);

// ✅ Force config to load from the same directory as the DLL
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddControllers();

// 1️⃣ Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Allow requests from any domain
              .AllowAnyHeader()    // Allow any headers
              .AllowAnyMethod();   // Allow GET, POST, etc.
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// 2️⃣ Add CORS middleware BEFORE Authorization
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
