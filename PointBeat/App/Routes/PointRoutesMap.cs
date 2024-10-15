using Microsoft.AspNetCore.Mvc;
using PointBeat.App.Dtos;
using PointBeat.App.Entities;
using PointBeat.App.Interfaces;
using PointBeat.App.Validators;
using Raven.Client.Documents;

namespace PointBeat.App.Routes;

public abstract class PointRoutesMap : IRoutesMap
{
    public static void Handle(WebApplication app)
    {
        app.MapPost("/points", (IDocumentStore store, [FromBody] PointDto dto) =>
        {
            var result = new PointDtoValidator().Validate(dto);
            if (!result.IsValid) return Results.BadRequest(result.Errors);

            var session = store.OpenSession();

            if (!session.Query<User>().Any(e => e.Id == dto.UserId))
                return Results.NotFound("user_not_found");
            if (!session.Query<Company>().Any(e => e.Id == dto.CompanyId))
                return Results.NotFound("company_not_found");

            var point = new Point
            {
                Hour = DateTime.Parse(dto.Hour),
                UserId = dto.UserId,
                CompanyId = dto.CompanyId
            };

            session.Store(point);
            session.SaveChanges();

            return Results.Created($"/users/{Uri.EscapeDataString(point.Id!)}", point);
        }).WithTags("Point");

        app.MapGet("/points", (IDocumentStore store) =>
        {
            var session = store.OpenSession();
            return Results.Ok(session.Query<Point>()
                .ToList());
        }).WithTags("Point");

        app.MapGet("/points/{id}", (IDocumentStore store, string id) =>
        {
            var session = store.OpenSession();
            var point = session.Load<Point>(Uri.UnescapeDataString(id));
            return point != null ? Results.Ok(point) : Results.NotFound();
        }).WithTags("Point");

        app.MapDelete("/points/{id}", (IDocumentStore store, string id) =>
        {
            var session = store.OpenSession();
            session.Delete(Uri.UnescapeDataString(id));
            session.SaveChanges();

            return Results.NoContent();
        }).WithTags("Point");
    }
}