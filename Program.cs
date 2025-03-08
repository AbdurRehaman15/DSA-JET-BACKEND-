using DsaJet.Api.Data;
using DsaJet.Api.Dto;
using DsaJet.Api.Entities;
using DsaJet.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultString"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultString"))
    ));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;  // Secure cookie access
    options.Cookie.IsEssential = true;  // Always store session cookie
});

var app = builder.Build();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/getAllProblems", async (AppDbContext db) =>
{
    var problems = await db.Problems
        .Include(p => p.Prerequisites)
        .Select(d => new GetProblemsDto
        {
            Name = d.Name,
            Description = d.Description,
            Difficulty = d.Difficulty,
            Tag = d.Tag,
        })
        .ToListAsync();

    return Results.Ok(problems);
});

app.MapGet("/getProblemsByTag/{tag}", async (AppDbContext db, string tag) => {
    var problems = await db.Problems
                   .Where(p => p.Tag == tag)
                   .Include(s => s.Solutions)
                   .Include(pr => pr.Prerequisites)
                   .Select(d => new GetProblemsDto 
                   {
                       Name = d.Name,
                       Description = d.Description,
                       Difficulty = d.Difficulty,
                       Tag = d.Tag,
                   }).ToListAsync();

    return Results.Ok(problems);
});

app.MapGet("getProblemByName/{Name}", async (AppDbContext db, string Name) => {
    
    var problem = await db.Problems.Where(p => p.Name == Name).Select(d => new GetProblemDto {
        Name = d.Name,
        Description = d.Description,
        Difficulty = d.Difficulty,
        Tag = d.Tag,
        VideoSolutionUrl = d.VideoSolutionUrl,
        Solutions = d.Solutions.Select(s => s.SolutionCode).ToList(),
        Prerequisites = d.Prerequisites.Select(pr => pr.Prereq).ToList()
    }).ToListAsync();

    return problem.Count == 0? Results.NotFound(new {message = "No problem with that name exists"}) : Results.Ok(problem);
 
});

app.MapGet("/getProblemSolution/{Name}/{Language}", async (AppDbContext db, string Name, string Language) => {
    var solution = await db.Solutions.Where(s => s.Problem_Name == Name && s.Language == Language).Select(s => s.SolutionCode).FirstOrDefaultAsync();;

    if(solution == null){
        return Results.NotFound();
    }

    return Results.Ok(new {Solution = solution});
});


app.MapGet("/users/{id}", async (AppDbContext db, int id) =>
{
    var user = await db.Users.FindAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapGet("getAllUsers", async (AppDbContext db) => {
    var users = await db.Users.Select(u => new UserDto {
        Username = u.Username,
        Email = u.Email,
    }).ToListAsync();

    return Results.Ok(users);
});

app.MapPost("/register", async (AppDbContext db, UserRegisterDto userDto) => {
    if(await db.Users.AnyAsync(u => u.Email == userDto.Email)){
        return Results.BadRequest("Email Already Registered");
    }

    if(await db.Users.AnyAsync(u => u.Username == userDto.Username)){
        return Results.BadRequest("User Name Already Exists");
    }

    var user = new User {
        Username = userDto.Username,
        Email = userDto.Email,
        PasswordHash = PasswordHasher.HashPassword(userDto.Password)
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", new {user.Id, user.Username, user.Email});

});

app.MapPost("/login", async (LoginDto request, AppDbContext db, IConfiguration config, HttpContext context) => {
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

    if(user == null){
        return Results.Unauthorized();
    }

    if(!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash)){
        return Results.Unauthorized();
    }

    context.Session.SetString("UseID", user.Id.ToString());
    context.Session.SetString("Username", user.Username);

    var token = JwtTokenHelper.GenerateToken(user.Username, config);

    return Results.Ok(new { Message = "User logged in succesfully",Token = token});
});



app.MapGet("getUserProfile",  (HttpContext context) => {
    var username = context.Session.GetString("Username");

    if(string.IsNullOrEmpty(username)) 
        return Results.Json(new { error = "Session expired or not logged in." }, statusCode: 401);

     
    return Results.Ok(new { Username = username, Message = "User is authenticated via session." });

});


app.MapPost("logout", async (HttpContext context) => {
    context.Session.Clear();

    return Results.Ok(new {Message = "User logged out succesfully"});
});


app.Run();

