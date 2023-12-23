using SHELF_LABEL_API_.NET7.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductDtlService>(provider =>
{
    return new ProductDtlService(builder.Configuration.GetConnectionString("OracleConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorePolicy", builder =>
    {
        builder.WithOrigins("http://192.168.51.26:86", "http://localhost:3000", "http://192.168.51.249:82", "http://192.168.51.252:82").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("MyCorePolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
