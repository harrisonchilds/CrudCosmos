using CrudCosmos.Models;
using System.Threading.Tasks;

namespace CrudCosmos.Services.Interfaces
{
    public interface IBookService : IService<Book>
    {
        public Task<bool> CheckForConflictingBook(Book book);
    }
}