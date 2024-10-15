using Microsoft.AspNetCore.Mvc;
using PointBeat.App.Entities;
using PointBeat.App.Interfaces;
using PointBeat.App.Validators;
using Raven.Client.Documents;

namespace PointBeat.App.Routes;

public abstract class CompanyRoutesMap : IRoutesMap
{
    public static void Handle(WebApplication app)
    {
        app.MapPost("/company", (IDocumentStore store, [FromBody] Company company) =>
        {
            var result = new CompanyValidator().Validate(company);
            if (!result.IsValid) return Results.BadRequest(result.Errors);

            var session = store.OpenSession();

            session.Store(company);
            session.SaveChanges();

            return Results.Created($"/company/{Uri.EscapeDataString(company.Id!)}", company);
        }).WithTags("Company");

        app.MapPut("/company/{id}", (IDocumentStore store, string id, [FromBody] Company company) =>
        {
            var result = new CompanyValidator().Validate(company);
            if (!result.IsValid) return Results.BadRequest(result.Errors);

            var session = store.OpenSession();

            var companyDb = session.Load<Company>(Uri.UnescapeDataString(id));
            if (companyDb == null) return Results.NotFound("company_not_found");

            companyDb.Name = company.Name;
            companyDb.Address = company.Address;

            session.Store(companyDb);
            session.SaveChanges();

            return Results.Ok(companyDb);
        });

        app.MapGet("/company", (IDocumentStore store) =>
        {
            var session = store.OpenSession();
            return Results.Ok(session.Query<Company>().ToList());
        }).WithTags("Company");

        app.MapGet("/company/{id}", (IDocumentStore store, string id) =>
        {
            var session = store.OpenSession();
            var company = session.Load<Company>(Uri.UnescapeDataString(id));

            return company != null ? Results.Ok(company) : Results.NotFound();
        }).WithTags("Company");

        app.MapDelete("/company/{id}", (IDocumentStore store, string id) =>
        {
            var session = store.OpenSession();
            session.Delete(Uri.UnescapeDataString(id));
            session.SaveChanges();

            return Results.NoContent();
        }).WithTags("Company");
    }
}