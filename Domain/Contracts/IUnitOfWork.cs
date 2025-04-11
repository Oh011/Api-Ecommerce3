using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {


        Task<int> SaveChangesAsync();



        //Function to Return Repository:

        // New GenericRepository<Product,int>
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
