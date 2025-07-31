using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LabMvc.Data;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseSqlite("Data Source=database.db") 
            .Options;

        return new Context(options);
    }
}