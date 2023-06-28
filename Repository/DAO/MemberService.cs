using Microsoft.Extensions.Configuration;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class MemberService : RepositoryBase<Member>
    {
        private readonly YogaCenterContext _context;

        public MemberService(YogaCenterContext context)
        {
            _context = context;
        }
        public Member GetAdminAccount()
        {
            Member Admin = null;
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                string email = config["Admin:email"];
                string password = config["Admin:password"];
                Admin = new Member
                {
                    MemberId = 0,
                    Email = email,
                    Username = "Admin",
                    FullName = "",
                    Phone = "",
                    Address = "",
                    Password = password
                };
            }
            return Admin;
        }
        public IEnumerable<Member> GetAllAccount()
        {
            IEnumerable<Member> listMember = null;
            try
            {
                listMember = _context.Members.ToList();
                Member admin = GetAdminAccount();
                listMember = listMember.Append(admin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listMember;
        }
    }
}
