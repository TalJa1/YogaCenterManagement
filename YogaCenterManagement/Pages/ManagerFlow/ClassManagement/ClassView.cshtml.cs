using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class ClassViewModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ClassService _classService;
        private readonly InstructorService _instructorService;
        private readonly RoomService _roomService;

        public ClassViewModel(MemberService memberService, ClassService classService, InstructorService instructorService, RoomService roomService)
        {
            _memberService = memberService;
            _classService = classService;
            _instructorService = instructorService;
            _roomService = roomService;
        }

        public IList<Class> Class { get; set; }

        public void OnGet()
        {
            if (_classService.GetAll() is not null)
            {
                Class = _classService.GetAll(include:x=>x.Include(a=>a.Room).Include(b=>b.Instructor)).ToList();
            }
        }
    }
}
