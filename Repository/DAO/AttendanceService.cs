﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class AttendanceService : RepositoryBase<Attendance>
    {
        public AttendanceService(YogaCenterContext context) : base(context)
        {
        }
    }
}