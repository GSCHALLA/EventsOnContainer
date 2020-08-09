﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Data
{
    public class MigrateDatabase
    {
        public static void EnsureCreated(OrderContext context)
        {
            context.Database.Migrate();
        }
    }
}
