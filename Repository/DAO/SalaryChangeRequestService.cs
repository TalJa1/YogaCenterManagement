using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class SalaryChangeRequestService : RepositoryBase<SalaryChangeRequest>
    {
        private readonly YogaCenterContext _context;

        public SalaryChangeRequestService(YogaCenterContext context)
        {
            _context = context;
        }
        public void AcceptSalaryRequest(int id)
        {
            var request = _context.SalaryChangeRequests.Find(id);
            if (request is not null)
            {
                request.IsApproved = true;
                _context.SalaryChangeRequests.Update(request);
                _context.SaveChanges();
            }
        }

        public void RejectSalaryRequest(int id)
        {
            var request = _context.SalaryChangeRequests.Find(id);
            if (request is not null)
            {
                request.IsApproved = false;
                _context.SalaryChangeRequests.Update(request);
                _context.SaveChanges();
            }
        }
    }
}
