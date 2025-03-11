using Microsoft.EntityFrameworkCore;
using Youth_Innovation_System.Core.Entities;
using Youth_Innovation_System.Core.Specifications;

namespace Youth_Innovation_System.Repository
{
    internal class SpecificationEvaluator<T> where T : BaseEntity
    {
        internal static IQueryable<T> BuildQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var outputQuery = inputQuery;

            if (spec.Criteria != null)
                outputQuery = outputQuery.Where(spec.Criteria);

            if (spec.OrderByAsc != null)
                outputQuery = outputQuery.OrderBy(spec.OrderByAsc);
            else if (spec.OrderByDesc != null)
                outputQuery = outputQuery.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPagingEnabled)
                outputQuery = outputQuery.Skip(spec.Skip.Value).Take(spec.Take.Value);

            // inputquery.Where(p => p.Id == 1).OrderByDescending(p => p.Id).Include(p => p.Id);
            if (spec.Includes.Count() > 0)
                outputQuery = spec.Includes.Aggregate(outputQuery, (currentQuery, include) => currentQuery.Include(include));

            return outputQuery;
        }
    }
}