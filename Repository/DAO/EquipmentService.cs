﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class EquipmentService : RepositoryBase<Equipment>
    {
        public EquipmentService(YogaCenterContext context) : base(context)
        {
        }
    }
}