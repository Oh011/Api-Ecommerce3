using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {




        Task<TEntity?> GetById(TKey id);


        Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking);


        Task<int> CountAsync(Specifications<TEntity> Specifications);
        Task AddAsync(TEntity entity);



        void Update(TEntity entity);
        void Delete(TEntity entity);


        //specifications

        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(Specifications<TEntity> Specification);
        Task<TEntity?> GetWithIDSpecificationsAsync(Specifications<TEntity> Specification);




    }
}

//--> <ProductRepo,int> 