using Microsoft.EntityFrameworkCore;

namespace CartProject.Infra.Data.Contexts
{
    public class InMemoryContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "in-memory-db");
        }
    }
}
