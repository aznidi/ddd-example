using SMS.Application.Commands.BillableServices;
using SMS.Application.Commands.Students;
using SMS.Application.Queries.BillableServices;
using SMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CreateStudentCommand>();
builder.Services.AddScoped<GetServicesQuery>();
builder.Services.AddScoped<CreateServiceCommand>();
builder.Services.AddScoped<UpdateServiceCommand>();


var app = builder.Build();


app.MapControllers();


app.Run();

