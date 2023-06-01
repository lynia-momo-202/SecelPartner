using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Interfaces;
using SecelPartner.infrastructure.Services;
using SecelPartner.Infrastructure.DefaultContext;
using SecelPartner.Infrastructure.Repositories;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;
using SecelPartner.UI.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region identity
var connectionString = builder.Configuration.GetConnectionString("SecelPartnerUIContextConnection") ?? throw new InvalidOperationException("Connection string 'SecelPartnerUIContextConnection' not found.");

builder.Services.AddDbContext<SecelPartnerUIContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SecelPartnerUIUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SecelPartnerUIContext>();
#endregion

#region DefaultContext
var connectionString1 = builder.Configuration.GetConnectionString("SecelPartnerDataContextConnection") ?? throw new InvalidOperationException("Connection string 'SecelPartnerDataContextConnection' not found.");

builder.Services.AddDbContext<SecelPartnerDataContext>(options =>
    options.UseSqlServer(connectionString1));
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

#region services
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IGerantRepository, GerantRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddSingleton<FichierService>();
builder.Services.AddSingleton<PathService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.MapRazorPages();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
