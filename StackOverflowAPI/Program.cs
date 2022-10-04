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
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddDbContext<StackOverflowDbContext>
    (o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<StackOverflowDbContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();

if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

if (!dbContext.Tags.Any())
{
    dbContext.Tags.AddRange(
        new List<Tag>(){
                        new Tag(){Text="C#"},
                        new Tag(){Text="EF Core"},
                        new Tag(){Text="Java"},
                        new Tag(){Text="C++"},
                        new Tag(){Text="Angular"},
                        new Tag(){Text="React"},
                        new Tag(){Text="JavaScript"},
        });

    dbContext.SaveChanges();
}

app.UseHttpsRedirection();

app.MapPost("/users", async (IUserService service, [FromBody] UserDto dto) => await service.AddNewUser(dto));
app.MapGet("/users", (IUserService service) => service.GetUsers());
app.MapGet("/users/{email}", async (IUserService service, string email) => await service.GetUser(email));
app.MapPut("/users", (IUserService service, [FromBody] UserDto dto) => service.UpdateUser(dto));

app.MapPost("/questions", async (IQuestionService service, [FromBody] CreateQuestionDto dto)
    => await service.AddNewQuestion(dto));
app.MapGet("/users/{email}/questions", (IQuestionService service, string email)
    => service.GetUserQuestions(email));

app.MapGet("/questions/{id}", (IQuestionService service, long id)
    => service.GetQuestion(id));

app.MapPost("/questions/{id}/answer", async (IMessageService service, long id, [FromBody] CreateMessageDto answer)
    => await service.AddChildMessage<Question, Answer>(id, answer));

app.MapPost("/questions/{id}/comment", async (IMessageService service, long id, [FromBody] CreateMessageDto comment)
    => await service.AddChildMessage<Question, Comment>(id, comment));

app.MapPost("/answers/{id}/comment", async (IMessageService service, long id, [FromBody] CreateMessageDto comment)
    => await service.AddChildMessage<Answer, Comment>(id, comment));

app.Run();