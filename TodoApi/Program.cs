using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

       
            var builder = WebApplication.CreateBuilder(args);

            // Configura��o do CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", policy =>
                {
                builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            }
                   
            
            });

            // Configura��o do Banco de Dados
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Adiciona os servi�os para os Controllers
            builder.Services.AddControllers();

            // Servi�os do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAnyOrigin");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); 
                app.UseSwaggerUI();       
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();
            
            app.MapControllers();
            
            app.Run();
  
    

