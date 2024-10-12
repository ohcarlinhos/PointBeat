using PointBeat.App.Routes;
using Raven.Client.Documents;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDocumentStore>((_) =>
{
    var store = new DocumentStore
    {
        Urls = ["http://localhost:8080"],
        Database = "PointBeat"
    };

    store.Initialize();
    return store;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

UserRoutesMap.Handle(app);
PointRoutesMap.Handle(app);
CompanyRoutesMap.Handle(app);

app.Run();