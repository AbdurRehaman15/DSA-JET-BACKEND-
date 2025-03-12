using System;
using DsaJet.Api.Data;
using DsaJet.Api.Dto;
using DsaJet.Api.Entities;
using DsaJet.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DsaJet.Api.Services;

public static class UserService
{
    public static void AddUserEndPoints(this WebApplication app){
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
        }).RequireAuthorization();

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

        app.MapPost("/login", async (LoginDto request, AppDbContext db, IConfiguration config) => {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if(user == null){
                return Results.Unauthorized();
            }

            if(!PasswordHasher.VerifyPassword(request.Password, user.PasswordHash)){
                return Results.Unauthorized();
            }
            
            var refrehToken = JwtTokenHelper.GenerateRefresherToken();
            var jwtToken = JwtTokenHelper.GenerateToken(user.Id, user.Username, config);

            var refTokenObj = new RefreshToken {
                UserId = user.Id,
                JwtRefresher = refrehToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            db.RefreshTokens.Add(refTokenObj);
            await db.SaveChangesAsync();

            return Results.Ok(new { Message = "User logged in succesfully", AccessToken = jwtToken, RefresherToken = refrehToken});
        });



        app.MapGet("/getUserProfile", (HttpContext context) => {
            var user = context.User; // Extract the user from JWT token

            if (user.Identity is not { IsAuthenticated: true })
            {
                return Results.Json(new { error = "Unauthorized: Invalid or missing token." }, statusCode: 401);
            }

            var username = user.Identity.Name;

            return Results.Ok(new { Username = username, Message = "User is authenticated via JWT." });
        }).RequireAuthorization();


        app.MapPost("/logout", () => {
            return Results.Ok(new {Message = "User logged out succesfully"});
        }).RequireAuthorization();
        

        app.MapGet("/refresh", async (AppDbContext db, HttpContext context, IConfiguration config) => {
            var recievedToken = context.Request.Headers["Refresh-Token"].ToString();

            if(string.IsNullOrEmpty(recievedToken)) {
                return Results.Unauthorized();
            }

            var userRefTokenObj  = await db.RefreshTokens.FirstOrDefaultAsync(u => u.JwtRefresher == recievedToken);

            if(userRefTokenObj == null || userRefTokenObj.ExpiresAt < DateTime.UtcNow){
                if(userRefTokenObj != null){
                    db.RefreshTokens.Remove(userRefTokenObj);
                    await db.SaveChangesAsync();
                }
                return Results.Unauthorized();
            }

            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userRefTokenObj.UserId);

            if(user == null){
                db.RefreshTokens.Remove(userRefTokenObj);
                await db.SaveChangesAsync();
            }
            
            var jwtToken = JwtTokenHelper.GenerateToken(user.Id, user.Username, config);
            
            return Results.Ok(new {AccessToken = jwtToken});
            
            
        });
    }
}
