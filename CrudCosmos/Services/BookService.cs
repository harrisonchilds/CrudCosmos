using CrudCosmos.Models;
using CrudCosmos.Repositories.Interfaces;
using CrudCosmos.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace CrudCosmos.Services
{
    public class BookService : Service<Book>, IBookService
    {
        public BookService(IRepository<Book> repository) : base(repository) { }

        public async Task<bool> CheckForConflictingBook(Book book)
        {
            return (await _repository.GetByCondition(x => x.Title == book.Title)).Any();
        }
    }
}