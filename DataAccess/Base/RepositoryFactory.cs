using System.Data.Objects;
using LongHu.DataAccess.EFImp;
namespace LongHu.DataAccess.Base
{
    public class RepositoryFactory<T> where T : class, new()
    {
        public static IRepository<T> CreateEfRepository(ObjectContext context)
        {
            IRepository<T> repository = new EfRepository<T>(context);
            return repository;
        }
    }
}

