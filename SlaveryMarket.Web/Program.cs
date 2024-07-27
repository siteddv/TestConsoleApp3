using Microsoft.OpenApi.Models;
using SlaveryMarket.Data.Repository;
using SlaveryMarket.Web;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.InitializeRepositories();
builder.Services.AddEndpointsApiExplorer();

//����������� ��������� Swagger ��� ����
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    //������� �������� � ������� v1 � ��������� SlaveryJanar
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "SlaveryJanar" });

    //����� ������������� ����� ������������ ��� JWT
    swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //���������, ��� ����� ����� ������������ � ��������� HTTP
        In = ParameterLocation.Header,
        //��������, ������� ������������ � ���������� Swagger, ��������, ��� ������� �����
        Description = "Please enter JWT with Bearer into field",
        //��� ���������, � ������� ����� ������������ �����
        Name = "Authotization",
        //��� ����� ������������(ApiKey ������������ ��� �������� ������)
        Type = SecuritySchemeType.ApiKey,
        //����� ������������ (Bearer ������������ ��� JWT).
        Scheme = "bearer",
    });

    //��������� ���������� ������������ � Swagger
    //�� ���� ��������� ��� ��� ������� � API �������� ��������� ����� 
    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                //���������, ��� ���� ���������� ��������� �� ����� ������������ �  Id = "Bearer"
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            //������ ������, ������� ���������, ��� ��� ����� ���������� �� �����
            //�����-���� ���������� ����� ��� ����������
            Array.Empty<string>()
        }
    });

    //����������� �������������� ����(������� ������) � Swagger
    //����� ��� ������������ ������ ����� �����, ��� ��������� ����������.
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
