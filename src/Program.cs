using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsDayScoring.Components;
using SportsDayScoring.Components.Account;
using SportsDayScoring.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// builder.Services.AddCascadingAuthenticationState();
// builder.Services.AddScoped<IdentityUserAccessor>();
// builder.Services.AddScoped<IdentityRedirectManager>();
// builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
//
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultScheme = IdentityConstants.ApplicationScheme;
//     options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
//     
// })
// .AddIdentityCookies();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=SportsDayScoring.db");
    options.EnableSensitiveDataLogging();
});

// builder.Services.AddIdentityCore<ApplicationUser>(options =>
//     {
//         options.Password.RequireNonAlphanumeric = false;
//     })
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddSignInManager()
//     .AddDefaultTokenProviders();

builder.Services.AddScoped<DataService>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
}
else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     dbContext.Database.Migrate();
// }
//
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

