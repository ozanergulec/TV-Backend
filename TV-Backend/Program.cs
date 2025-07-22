using TV_Backend.Services.HotelProduct;
using TV_Backend.Services;
using TV_Backend.Services.Booking;
using StackExchange.Redis;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Response Compression Configuration
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    
    // JSON responses için compression
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});

// Compression levels - hızlı sıkıştırma
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TV-Backend API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    
    // Custom schema ID'leri ekle
    c.CustomSchemaIds(type => type.Name);
});

builder.Services.AddHttpClient();

// Redis Cache Registration
var redisConnectionString = builder.Configuration["Redis:ConnectionString"] ?? "localhost:6379";
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
});
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

// Service Registrations 
builder.Services.AddScoped<HotelProductService>(); 
builder.Services.AddScoped<GetOffersService>(); 
builder.Services.AddScoped<LookupService>();
builder.Services.AddScoped<GetOfferDetailsService>();

builder.Services.AddScoped<IBeginTransactionService, BeginTransactionService>();
builder.Services.AddScoped<ISetReservationInfoService, SetReservationInfoService>();
builder.Services.AddScoped<ICommitTransactionService, CommitTransactionService>();
builder.Services.AddScoped<GetReservationDetailService>();

// Token Service Interface Registration
builder.Services.AddSingleton<ISanTsgTokenService, SanTsgTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Response Compression Middleware - DİKKAT: diğer middleware'lerden ÖNCE!
app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
