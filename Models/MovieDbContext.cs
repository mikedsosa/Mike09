using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mike09.Models
{
	//create a context file to act as the intermediary between the app and the DB
	public class MovieDbContext : DbContext
	{
		public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
		{

		}
		public DbSet<Movie> Movies { get; set; }
	}
}
