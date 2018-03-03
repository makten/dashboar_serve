using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using dashboard.Core.Models;

namespace dashboard.Extensions
{
    public static class IQuerableExtensions
    {

        // Enable Sorting of content
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj,  Dictionary<string, Expression<Func<T, object>>> columnsMap )
        {
            //Check for null or whitespace and if key exists
            if(String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            if(queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else                
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        //Enables Pagination of content
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, IQueryObject queryObj){

            queryObj.Page = queryObj.Page <= 0 ? 1 : queryObj.Page;
            queryObj.PageSize = queryObj.PageSize <= 0 ? (byte) 10 :  queryObj.PageSize;
            
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize ).Take(queryObj.PageSize);
        }
        
    }
}