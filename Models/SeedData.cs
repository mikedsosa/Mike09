using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mike09.Models
{
    //make some seed data to make sure the DB builds correctly 
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder application)
        {
            //grab an instance of our CharitDbContext using a scoped version of it
            MovieDbContext context = application.ApplicationServices.
                CreateScope().ServiceProvider.GetRequiredService<MovieDbContext>();

            //if there are any pending migrations, migrate!
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            //if there is nothing in the database yet...
            if (!context.Movies.Any())
            {
                //then add all this stuff:
                context.Movies.AddRange(
                new Movie
                {
                    Category = "EXAMPLE DATA",
                    Title = "EXAMPLE DATA",
                    Year = "99999999999",
                    Director = "EXAMPLE DATA",
                    Rating = "EXAMPLE DATA",
                    Edited = false,
                    LentTo = "EXAMPLE DATA",
                    Notes = "EXAMPLE DATA"
                }

                );

                //go write this to the database
                context.SaveChanges();
            }
        }
    }
}
