using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ShopContext>();
    context.Database.Migrate();
    DataSeeder.SeedData(context); 
}


app.Run();