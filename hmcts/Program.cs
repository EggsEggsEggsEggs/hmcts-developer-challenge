using GovUk.Frontend.AspNetCore;

using hmcts.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<HmctsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("hmctsContext") ?? throw new InvalidOperationException("Connection string 'hmctsContext' not found.")));

builder.Services.AddGovUkFrontend();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
