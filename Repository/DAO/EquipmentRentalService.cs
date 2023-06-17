﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class EquipmentRentalService : RepositoryBase<EquipmentRental>
    {
        public EquipmentRentalService(YogaCenterContext context) : base(context)
        {
        }
    }
}