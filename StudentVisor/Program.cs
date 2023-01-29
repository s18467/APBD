using StudentVisor.Models;
using StudentVisor.Repositories;

namespace StudentVisor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Metoda AddControllers zastępuje już większosć kodu z klasy Startup, przez co nie jest ona potrzebna
            var sc = builder.Services;
            sc.AddControllers();

            sc.AddSingleton<IDataParser<Student>>(new StudentDataParser());
            sc.AddSingleton<IDataFileHandler<Student>, DataFileHandler<Student>>();
            sc.AddSingleton<IRepository<Student>, StudentRepository>();

            //builder.Services.AddDbContext<StudentVisorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentVisorContext")));
            sc.AddAntiforgery();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            sc.AddEndpointsApiExplorer();
            sc.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.yaml", "StudentVisor v1"));
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}