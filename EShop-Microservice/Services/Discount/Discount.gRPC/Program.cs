var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteDatabase")));


var app = builder.Build();

app.UseMigration();

app.MapGrpcService<DiscountService>();

app.Run();
