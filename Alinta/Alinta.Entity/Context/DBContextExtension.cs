using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alinta.Core.Entities;
using Alinta.Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace Alinta.Entity.Context
{
    public static class DbContextExtension
    {

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
            //return false;
        }

        public static void EnsureSeeded(this AlintaContext context)
        {

            //Ensure we have some status
            if (!context.Customers.Any())
            {
                var customers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(@"seed" + Path.DirectorySeparatorChar + "customers.json"));
                context.AddRange(customers);
                context.SaveChanges();
            }
        }

    }
}
