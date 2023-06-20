using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.DAO;
using Repository.Models;

namespace YogaCenterManagement.Pages.ManagerFlow
{
    public class DeleteModel : PageModel
    {
        private readonly ClassService _classService;

        public DeleteModel(ClassService classService)
        {
            _classService = classService;
        }

        [BindProperty]
        public Class Class { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null || _classService.GetAll() == null)
            {
                return NotFound();
            }

            Class = _classService.GetAll().FirstOrDefault(m => m.ClassId == id);

            if (Class == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class = _classService.getById(id);

            if (Class != null)
            {
                _classService.Delete(Class);
            }

            return RedirectToPage("./Index");
        }
    }
}
