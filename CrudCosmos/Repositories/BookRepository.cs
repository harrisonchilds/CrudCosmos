using CrudCosmos.Models;
using CrudCosmos.Repositories.Interfaces;

namespace CrudCosmos.Repositories
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(CosmosContext dbContext) : base(dbContext) { }
    }
}