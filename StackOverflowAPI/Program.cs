using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflowAPI.Dtos;
using StackOverflowAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(StackOverflowDbContext).Assembly);

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

app.MapPost("/users", async (StackOverflowDbContext db, IMapper mapper, [FromBody] UserDto dto) =>
{
    if (db.Users.Any(u => u.Email.ToLower().Trim() == dto.Email.ToLower()))
    {
        throw new Exception("Email exists in database!");
    }
    else
    {
        dto.Email = dto.Email.Trim();
        var user = mapper.Map<User>(dto);

        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return user.Id;
    }
});

app.Run();