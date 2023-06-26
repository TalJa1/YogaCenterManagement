using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;
using YogaCenterManagement.FluentValidation.ClassValidation;

namespace YogaCenterManagement
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
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
            services.AddScoped<SlotService>();
            services.AddScoped<ClassChangeRequestService>();
            services.AddScoped<CartService>();
            //Register FluentValidation
            services.AddScoped<IValidator<Class>, CreateClassValidation>();
            services.AddSession();
            return services;
        }
    }
}
