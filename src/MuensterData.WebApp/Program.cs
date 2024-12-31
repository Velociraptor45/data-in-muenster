using MuensterData.Domain;
using MuensterData.Infrastructure;
using MuensterData.WebApp.Localization;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddDomain();
builder.Services.AddInfrastructure();

builder.Services.AddSingleton<ISyncfusionStringLocalizer, SyncfusionLocalizer>();

SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SfLicenseKey"]);

var app = builder.Build();
app.UseRequestLocalization("de-DE");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
