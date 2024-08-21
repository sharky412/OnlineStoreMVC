using Microsoft.EntityFrameworkCore;
using OnlineStoreMVC.Models;

namespace OnlineStoreMVC.Services
{
    public class ApplicationDbContex :DbContext
    {
        public ApplicationDbContex(DbContextOptions options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
