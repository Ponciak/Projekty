using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Database.Entities;

namespace ShallowLibAPI.Models
{
    public static class DBinitializer
    {
        public static void Seed(Database.DatabaseContext context)
        {
            if (!context.Autors.Any())
            {
                context.AddRange(new Autor { Name = "Adam Mickiewicz" });

            }
            context.SaveChanges();
        }

    }
}
