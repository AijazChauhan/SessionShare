using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SystemWebAdapters.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Services.AddAuthentication()
//    .AddCookie("SharedCookie", options => options.Cookie.Name = ".AspNet.ApplicationCookie");

builder.Services.AddSystemWebAdapters()
    .AddJsonSessionSerializer(options =>
    {
        options.KnownKeys.Add("Test", typeof(string));
    })
    .AddRemoteAppClient(options =>
    {
        options.RemoteAppUrl = new(builder.Configuration["ReverseProxy:Clusters:fallbackCluster:Destinations:fallbackApp:Address"]);
        options.ApiKey = "e411badc-f72a-4414-b470-a94eabd7d5a4";
    })

    //.AddAuthenticationClient(true)
    .AddSessionClient();


//builder.Services.AddAuthentication(RemoteAppAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(IdentityConstants.ApplicationScheme, options =>
//    {
//        options.Cookie.Name = ".AspNet.ApplicationCookie";
//        options.Cookie.SameSite = SameSiteMode.Lax;
//        options.Cookie.Path = "/";
//        options.Cookie.HttpOnly = true;
//        options.Cookie.IsEssential = true;
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
//    });



// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSystemWebAdapters();
app.MapControllerRoute("Default", "{controller=AspNetCoreSession}/{action=Index}/{id?}")
    .RequireSystemWebAdapterSession();
//app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);
app.MapReverseProxy();
app.Run();
