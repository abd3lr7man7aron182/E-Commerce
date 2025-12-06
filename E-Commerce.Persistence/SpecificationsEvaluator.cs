using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationsEvaluator
    {
        //create query-build query
        //dbcontext.products.include(p=>p.producttype).include(p=>p.productbrand)
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecification<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = EntryPoint;//dbcontext.products
            if (specifications is not null)
            {
                if (specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);//dbcontext.products.where()
                }

                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    //foreach( var IncludeExp in specifications.IncludeExpressions)
                    //{

                    //    query = query.Include(IncludeExp); 
                    //}

                    query = specifications.IncludeExpressions.Aggregate(query,
                        (currentQuery, includeExp) => currentQuery.Include(includeExp));
                    //dbcontext.products  ->currentQuery
                    //p=>p.producttype    ->includeExp
                    //after that
                    //dbcontext.products.include(p=>p.producttype) ->currentQuery
                    //p=>p.productbrand       ->includeExp
                    //after that 
                    //dbcontext.products.include(p=>p.producttype).include(p=>p.productbrand) ->final query



                }

                if (specifications.OrderBy is not null)
                {
                    query = query.OrderBy(specifications.OrderBy);
                }

                if (specifications.OrderByDescending is not null)
                {
                    query = query.OrderByDescending(specifications.OrderByDescending);
                }
                if (specifications.IsPaginated)
                {
                    query = query.Skip(specifications.Skip).Take(specifications.Take);
                }

            }
            return query;
        }
    }
}
