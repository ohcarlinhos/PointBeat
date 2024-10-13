using Microsoft.AspNetCore.Mvc;
using PointBeat.App.Entities;
using PointBeat.App.Interfaces;
using PointBeat.App.Validators;
using Raven.Client.Documents;

namespace PointBeat.App.Routes;

public abstract class UserRoutesMap : IRoutesMap
{
    public static void Handle(WebApplication app)
    {
        app.MapPost("/users", (IDocumentStore store, [FromBody] User user) =>
        {
            var result = new UserValidator().Validate(user);
            if (!result.IsValid) return Results.BadRequest(result.Errors);

            var session = store.OpenSession();

            session.Store(user);
            session.SaveChanges();

            return Results.Created($"/users/{Uri.EscapeDataString(user.Id!)}", user);
        }).WithTags("User");

        app.MapGet("/users", (IDocumentStore store) =>
        {
            var session = store.OpenSession();
            return Results.Ok(session.Query<User>().ToList());
        }).WithTags("User");

        app.MapGet("/users/{id}", (IDocumentStore store, string id) =>
        {
            var session = store.OpenSession();
            var user = session.Load<User>(Uri.UnescapeDataString(id));
            return user != null ? Results.Ok(user) : Results.NotFound();
        }).WithTags("User");

        app.MapDelete("/users/{id}", (IDocumentStore store, string id) =>
        {
            var session = store.OpenSession();
            session.Delete(Uri.UnescapeDataString(id));
            session.SaveChanges();

            return Results.NoContent();
        }).WithTags("User");
    }
}