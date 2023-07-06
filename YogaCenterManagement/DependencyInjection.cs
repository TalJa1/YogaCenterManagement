using FluentValidation;
using Repository.DAO;
using Repository.Models;
using Repository.ViewModels;
using YogaCenterManagement.FluentValidation.ClassValidation;
using YogaCenterManagement.FluentValidation.EquipmentValidation;
using YogaCenterManagement.FluentValidation.InstructorValidation;
using YogaCenterManagement.FluentValidation.MemberValidation;
using YogaCenterManagement.FluentValidation.RoomValidation;
using YogaCenterManagement.FluentValidation.SalaryChangeRequestValidation;
using YogaCenterManagement.Pages.ManagerFlow.SalaryRequestChange;

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
            services.AddScoped<YogaCenterContext>();
            //Register FluentValidation
            services.AddScoped<IValidator<CreateClassViewModel>, CreateClassValidation>();
            services.AddScoped<IValidator<Equipment>, CreateEquipmentValidation>();
            services.AddScoped<IValidator<UpdateEquipmentViewModels>, UpdateEquipmentValidation>();
            services.AddScoped<IValidator<SalaryChangeRequest>, CreateSalaryChangeRequestValidation>();
            services.AddScoped<IValidator<Room>, CreateRoomValidation>();
            services.AddScoped<IValidator<Member>, CreateMemberValidation>();
            services.AddScoped<IValidator<Instructor>, CreateInstructorValidation>();
            services.AddSession();
            return services;
        }
    }
}
