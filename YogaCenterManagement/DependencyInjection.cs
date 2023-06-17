using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<YogaCenterContext>();
            services.AddScoped<AttendanceService>();
            services.AddScoped<ClassService>();
            services.AddScoped<EnrollmentService>();
            services.AddScoped<EquipmentService>();
            services.AddScoped<EquipmentRentalService>();
            services.AddScoped<EventRequestService>();
            services.AddScoped<InstructorService>();
            services.AddScoped<MemberService>();
            services.AddScoped<RoomService>();
            services.AddScoped<SalaryChangeRequestService>();
            services.AddScoped<PaymentService>();

            services.AddRazorPages();
            services.AddSession();
            return services;
        }
    }
}
