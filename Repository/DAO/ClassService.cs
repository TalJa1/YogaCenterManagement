using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class ClassService : RepositoryBase<Class>
    {
        public ClassService(YogaCenterContext context) : base(context)
        {
        }
    }
}
