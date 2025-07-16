using TV_Backend.Services.HotelProduct;
using TV_Backend.Services;
using TV_Backend.Services.Booking;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
builder.Services.AddScoped<HotelProductService>(); 
builder.Services.AddScoped<GetOffersService>(); 
builder.Services.AddScoped<LookupService>();
builder.Services.AddScoped<GetOfferDetailsService>();
builder.Services.AddScoped<IBeginTransactionService, BeginTransactionService>(); // BeginTransaction service için
builder.Services.AddScoped<ISetReservationInfoService, SetReservationInfoService>(); // SetReservationInfo service eklendi
builder.Services.AddScoped<ICommitTransactionService, CommitTransactionService>(); // CommitTransaction service eklendi
builder.Services.AddSingleton<SanTsgTokenService>(); //login işlemininin sürekli yapılması için

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
