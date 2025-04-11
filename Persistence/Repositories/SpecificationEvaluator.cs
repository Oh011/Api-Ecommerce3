using Domain.Contracts;

namespace Persistence.Repositories
{
    public static class SpecificationEvaluator<T>
    {



        public static IQueryable<T> GetQuery<T>(IQueryable<T> InputQuery, Specifications<T> Specifications) where T : class
        {


            // Get Input query

            var query = InputQuery;

            //Criteria 


            if (Specifications.Criteria != null)
            {

                query = query.Where(Specifications.Criteria);
            }


            //Include 


            if (Specifications.OrderBy != null)
            {

                query = query.OrderBy(Specifications.OrderBy);
            }

            else if (Specifications.OrderByDescending != null)
            {

                query = query.OrderByDescending(Specifications.OrderByDescending);
            }

            if (Specifications.IsPaginated == true)
            {

                query = query.Skip(Specifications.Skip).Take(Specifications.Take);
            }

            foreach (var item in Specifications.IncludeExpressions)
            {

                query = query.Include(item);

            }

            return query;


        }



    }
}
