using Labb3_API_WebbapplicationsWithC_ASP.NET.Data;
using Labb3_API_WebbapplicationsWithC_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Labb3_API_WebbapplicationsWithC_ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Return all persons
            app.MapGet("/persons", async (ApplicationDbContext context) =>
            {
                var person = await context.Persons
                .Include(p => p.PersonInterests)
                    .ThenInclude(i => i.Interest)
                    .ThenInclude(i => i.Links)
                .ToListAsync();
                if (person == null || !person.Any())
                {
                    return Results.NotFound("No persons found");
                }
                return Results.Ok(person);
            });

            // Create person
            app.MapPost("/persons", async (Person person, ApplicationDbContext context) =>
            {
                context.Persons.Add(person);
                await context.SaveChangesAsync();
                return Results.Created($"/person/{person.Id}", person);
            });

            // Get person by Id
            app.MapGet("/persons/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var person = await context.Persons
                .Include(p => p.PersonInterests)
                    .ThenInclude(pi => pi.Interest)
                    .ThenInclude(i => i.Links)
                .FirstOrDefaultAsync(p => p.Id == id);
                if (person == null)
                {
                    return Results.NotFound("People not found");
                }
                return Results.Ok(person);
            });

            // Edit person
            app.MapPut("/persons/{id:int}", async (int id, Person updatedPerson, ApplicationDbContext context) =>
            {
                var person = await context.Persons.FindAsync(id);
                if (person == null)
                {
                    return Results.NotFound("Person not found");
                }
                person.FirstName = updatedPerson.FirstName;
                person.LastName = updatedPerson.LastName;
                person.Email = updatedPerson.Email;
                person.PhoneNumber = updatedPerson.PhoneNumber;
                context.Persons.Update(person);
                await context.SaveChangesAsync();
                return Results.Ok(person);
            });

            // Delete person
            app.MapDelete("/persons/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var person = await context.Persons.FindAsync(id);

                if (person == null)
                {
                    return Results.NotFound("Person not found");
                }
                context.Persons.Remove(person);
                await context.SaveChangesAsync();
                return Results.Ok($"Person with ID: {id} was deleted");
            });

            // Return all interests
            app.MapGet("/interests", async (ApplicationDbContext context) =>
            {
                var interests = await context.Interests.ToListAsync();
                if (interests == null || !interests.Any())
                {
                    return Results.NotFound("No interests found");
                }
                return Results.Ok(interests);
            });

            // Create interest
            app.MapPost("/interests", async (Interest interest, ApplicationDbContext context) =>
            {
                context.Interests.Add(interest);
                await context.SaveChangesAsync();
                return Results.Created($"/interests/{interest.Id}", interest);
            });

            // Delete interest
            app.MapDelete("/interests/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var interest = await context.Interests.FindAsync(id);

                if (interest == null)
                {
                    return Results.NotFound("Interest not found");
                }
                context.Interests.Remove(interest);
                await context.SaveChangesAsync();
                return Results.Ok($"Interest with ID: {id} was deleted");
            });

            // Return all PersonInterests
            app.MapGet("/personinterests", async (ApplicationDbContext context) =>
            {
                var personInterests = await context.PersonInterests
                .Include(pi => pi.Interest)
                .ToListAsync();
                if (personInterests == null || !personInterests.Any())
                {
                    return Results.NotFound("No personinterest found");
                }
                return Results.Ok(personInterests);
            });

            // Create PersonInterest
            app.MapPost("/personinterests", async (PersonInterest personInterests, ApplicationDbContext context) =>
            {
                context.PersonInterests.Add(personInterests);
                await context.SaveChangesAsync();
                return Results.Created($"/personinterests/{personInterests.Id}", personInterests);
            });

            // Delete PersonInterests
            app.MapDelete("/personinterests/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var personInterests = await context.PersonInterests.FindAsync(id);

                if (personInterests == null)
                {
                    return Results.NotFound("PersonInterests not found");
                }
                context.PersonInterests.Remove(personInterests);
                await context.SaveChangesAsync();
                return Results.Ok($"PersonInterests with ID: {id} was deleted");
            });

            // Create Link
            app.MapPost("/links", async (Link link, ApplicationDbContext context) =>
            {
                context.Links.Add(link);
                await context.SaveChangesAsync();
                return Results.Created($"/links/{link.Id}", link);
            });

            // Delete Link
            app.MapDelete("/links/{id:int}", async (int id, ApplicationDbContext context) =>
            {
                var link = await context.Links.FindAsync(id);

                if (link == null)
                {
                    return Results.NotFound("Link not found");
                }
                context.Links.Remove(link);
                await context.SaveChangesAsync();
                return Results.Ok($"Link with ID: {id} was deleted");
            });

            app.Run();
        }
    }
}