using InvoiceManagement.API.Middlewares;
using InvoiceManagement.API.Utils;
using InvoiceManagement.BLL.Services;
using InvoiceManagement.BLL.Services.Interfaces;
using InvoiceManagement.DAL.Context;
using InvoiceManagement.DAL.Repositories;
using InvoiceManagement.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProject", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

    c.AddSecurityRequirement(securityRequirement);

});

builder.Services.AddDbContext<DbContext,LibraryDbContext>();

builder.Services.AddScoped<IInvoiceManagementRepository, InvoiceManagementRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<IInvoiceProductManagementRepository, InvoiceProductManagementRepository>();

builder.Services.AddScoped<ICustomerManagementRepository, CustomerManagementRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductManagementRepository, ProductManagementRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<JwtUtils>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenInfo").GetSection("secret").Value!)),
        ValidateIssuer = false,
        ValidIssuer = builder.Configuration.GetSection("TokenInfo").GetSection("issuer").Value,
        ValidateAudience = false,
        ValidAudience = builder.Configuration.GetSection("TokenInfo").GetSection("audience").Value,
        ValidateLifetime = false
    };
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Auth", p => p.RequireAuthenticatedUser());
    o.AddPolicy("Admin", p => p.RequireRole("Admin"));
});

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); //gpt 
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();