var builder = WebApplication.CreateBuilder(args);

const string CorsPolicy = "vite-dev";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy, policy =>
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(CorsPolicy);

app.UseHttpsRedirection();

var answers = new[]
{
    "It is certain",
    "It is decidedly so",
    "Without a doubt",
    "Yes definitely",
    "You may rely on it",
    "As I see it, yes",
    "Most likely",
    "Outlook good",
    "Yes",
    "Signs point to yes",
    "Reply hazy, try again",
    "Ask again later",
    "Better not tell you now",
    "Cannot predict now",
    "Concentrate and ask again",
    "Don't count on it",
    "My reply is no",
    "My sources say no",
    "Outlook not so good",
    "Very doubtful",
};

app.MapGet("/answer", () =>
{
    string answer = answers[Random.Shared.Next(answers.Length)];
    return Results.Json(new { answer });
}).RequireCors(CorsPolicy);

//.WithName("answer");

app.Urls.Add("http://localhost:3000");
app.Run();

