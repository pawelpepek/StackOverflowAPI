using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;
using StackOverflowAPI.Interfaces;
using StackOverflowAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(StackOverflowDbContext).Assembly);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IMessageFinderService, MessageFinderService>();

builder.Services.AddDbContext<StackOverflowDbContext>
    (o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/users", async (IUserService service, [FromBody] UserDto dto) => await service.AddNewUser(dto));
app.MapGet("/users", (IUserService service) => service.GetUsers());
app.MapGet("/users/{email}", async (IUserService service, string email) => await service.GetUser(email));
app.MapPut("/users", (IUserService service, [FromBody] UserDto dto) => service.UpdateUser(dto));

app.MapPost("/users/{email}/questions", async (IQuestionService service,string email,  [FromBody] string content) 
    => await service.AddNewQuestion(email,content));
app.MapGet("/users/{email}/questions", (IQuestionService service, string email)
    => service.GetUserQuestions(email));

app.MapPost("/question/{id}/answer", async (IQuestionService service, long id, [FromBody] CreateAnswerDto answer)
    => await service.AddAnswer(id, answer));


app.Run();