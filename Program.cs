using Demo.Core.Api.Models;
using Demo.Core.Api.TenantService;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/* added our dbcontext to dependcy injection*/
//builder.Services.AddDbContext<BrandContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("brandConnectionStrings")));
builder.Services.AddDbContext<BrandContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TenantProvider>();
builder.Services.Configure<TenantConnectionString>(options => builder.Configuration.GetSection(nameof(TenantConnectionString)).Bind(options));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionStrings");
    options.InstanceName = "localRedis_";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
