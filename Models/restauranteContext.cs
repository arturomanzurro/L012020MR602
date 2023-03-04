using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L012020MR602.Models
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options) 
        {

        }
    }
}
