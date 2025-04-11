using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {



        //The filtering condition (like WHERE)
        public Expression<Func<T, bool>>? Criteria { get; }


        // 2. List of navigation properties to include (like ProductType, ProductBrand)
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();


        // 3. Order By Ascending

        public Expression<Func<T, object>>? OrderBy { get; private set; }


        // 3. Order By Descending


        public Expression<Func<T, object>>? OrderByDescending { get; private set; }


        //

        public int Take { get; private set; }


        public int Skip { get; private set; }


        public bool IsPaginated { get; private set; }





        protected Specifications(Expression<Func<T, bool>> Criteria)
        {



            this.Criteria = Criteria;
        }


        protected void AddInclude(Expression<Func<T, object>> Expression)
            => IncludeExpressions.Add(Expression);



        protected void SetOrderBy(Expression<Func<T, object>> Expression) => OrderBy = Expression;



        protected void SetOrderByDescending(Expression<Func<T, object>> Expression) => OrderByDescending = Expression;



        protected void ApplyPagination(int PageIndex, int PageSize)
        {
            this.IsPaginated = true;
            Take = PageSize;

            Skip = (PageIndex - 1) * PageSize;
        }

    }
}
