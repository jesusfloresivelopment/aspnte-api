using System;
using Microsoft.EntityFrameworkCore;
namespace lab1_api.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options )
            :base(options)
        {

        }
        public DbSet<Author> Authors {get; set;}
        public DbSet<Book> Books {get; set;}
        
    }
    
}