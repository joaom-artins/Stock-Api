using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {    
        }

        public DbSet<Stock> Stocks {get;set;}
        public DbSet<Comment> Comments {get;set;}

    }
}