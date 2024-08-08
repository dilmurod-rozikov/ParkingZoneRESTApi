using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.DataAccess;
using ParkingZoneWebApi.Repository;
using ParkingZoneWebApi.Repository.Interfaces;
using ParkingZoneWebApi.Services;
using ParkingZoneWebApi.Services.Interfaces;

namespace ParkingZoneWebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

            builder.Services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();
            builder.Services.AddScoped<IParkingZoneService, ParkingZoneService>();

            builder.Services.AddScoped<IParkingSlotService, ParkingSlotService>();
            builder.Services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();

            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
