using ENSEK.Classes.Helpers;

var builder = WebApplication.CreateBuilder(args);

ConfigurationHelper.Initialize(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Just to check we can connect
//string connectionString = app.Configuration.GetConnectionString("ENSEKConnectionString");
//try
//{
//    using var conn = new SqlConnection(connectionString);
//    conn.Open();
//}
//catch (Exception e)
//{
//    Console.WriteLine(e.Message);
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
