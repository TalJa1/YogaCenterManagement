﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class EnrollmentService : RepositoryBase<Enrollment>
    {
        public EnrollmentService(YogaCenterContext context) : base(context)
        {
        }
    }
}