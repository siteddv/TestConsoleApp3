using Microsoft.OpenApi.Models;
using SlaveryMarket.Data.Repository;
using SlaveryMarket.Web;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.InitializeRepositories();
builder.Services.AddEndpointsApiExplorer();

//Настраиваем генерицию Swagger под себя
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    //Создаем страницу с версией v1 и названием SlaveryJanar
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "SlaveryJanar" });

    //Здесь настраивается схема безопасности для JWT
    swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //Указываем, что токен будет передаваться в заголовке HTTP
        In = ParameterLocation.Header,
        //Описание, которое отобразиться в интерфейсе Swagger, объясняя, как вводить токен
        Description = "Please enter JWT with Bearer into field",
        //Имя заголовка, в котором будет передаваться токен
        Name = "Authotization",
        //Тип схемы безопасности(ApiKey используется для передачи токена)
        Type = SecuritySchemeType.ApiKey,
        //Схема аунтификация (Bearer используется для JWT).
        Scheme = "bearer",
    });

    //добавляем требование безопасности к Swagger
    //То есть указываем что для доступа к API запросам Требуется Токен 
    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                //Указываем, что наше требование ссылается на схему безопасности с  Id = "Bearer"
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            //Пустой массив, который указывает, что для этого требования не нужно
            //каких-либо конкретных ролей или разрешений
            Array.Empty<string>()
        }
    });

    //Настраиваем идентификаторы схем(моделей данных) в Swagger
    //чтобы они использовали полные имена типов, для избежания канфликтов.
    swaggerGenOptions.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();
// ---------------------------------------------------------
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ---------------------------------------------------------
app.Run();
