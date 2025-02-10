using WebProjrctManagement.Data;
using WebProjrctManagement.Validator;
using WebProjrctManagement.Model;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(provider =>
{
    var account = new CloudinaryDotNet.Account(
        "dmvc68a6b",
        "669761883156858",
        "QLai-7vKb1bS_WsL_BcI8xW5HEY"
    );
    return new CloudinaryDotNet.Cloudinary(account);
});
// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<FacultyValidation>();
        fv.RegisterValidatorsFromAssemblyContaining<MeetingValidation>();
        fv.RegisterValidatorsFromAssemblyContaining<ProjectValidation>();
        fv.RegisterValidatorsFromAssemblyContaining<StudentProjectValidation>();
        fv.RegisterValidatorsFromAssemblyContaining<StudentValidation>();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<StudentsRepo>();
builder.Services.AddScoped<FacultyRepo>();
builder.Services.AddScoped<ProjectRepo>();
builder.Services.AddScoped<MeetingRepo>();
builder.Services.AddScoped<StudentProjectRepo>();
builder.Services.AddScoped<StudentWorkRepo>();
builder.Services.AddScoped<DashboardRepo>();
builder.Services.AddScoped<StudentTaskRepo>();



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
