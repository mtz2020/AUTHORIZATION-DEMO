using AuthorizationDemo.Authentication;
using AuthorizationDemo.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddAuthentication("Basic")
                .AddScheme<BasicAuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", opt => { });

builder.Services.AddSingleton<IAuthorizationHandler, SuperAdminAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserMustBeSuperAdmin", configurePolicy => configurePolicy.Requirements.Add(new SuperAdminRequirement()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
