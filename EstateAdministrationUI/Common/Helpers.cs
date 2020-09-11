using System;
using System.Collections.Generic;
using System.Linq;

namespace EstateAdministrationUI.Common
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Areas.Estate.Models;
    using Microsoft.AspNetCore.Http;
    using Shared.Logger;
    using BusinessLogic.Common;

    [ExcludeFromCodeCoverage]
    public class Helpers
    {
        /// <summary>
        /// Gets the data for data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formData">The form data.</param>
        /// <param name="queryData">The query data.</param>
        /// <param name="whereClause">The where clause.</param>
        /// <returns></returns>
        public static DataTablesResult<T> GetDataForDataTable<T>(IFormCollection formData,
                                                                  IEnumerable<T> queryData,
                                                                  Expression<Func<T, Boolean>> whereClause = null)
        {
            DataTablesResult<T> result;

            if (formData == null)
            {
                result = null;
            }
            else
            {
                Logger.LogInformation("got form");
                // Extract the data tables fields
                String draw = formData["draw"].FirstOrDefault();
                Logger.LogInformation($"draw is {draw}");
                // Skiping number of Rows count  
                String start = formData["start"].FirstOrDefault();
                Logger.LogInformation($"start is {start}");
                // Paging Length 10,20  
                String length = formData["length"].FirstOrDefault();
                Logger.LogInformation($"length is {length}");
                // Sort Column Name  
                String sortColumn = formData["columns[" + formData["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                Logger.LogInformation($"sortcol is {sortColumn}");
                // Sort Column Direction ( asc ,desc)  
                String sortColumnDirection = formData["order[0][dir]"].FirstOrDefault();
                Logger.LogInformation($"sortcoldirection is {sortColumnDirection}");
                // Search Value from (Search box)  
                String searchValue = formData["search[value]"].FirstOrDefault();
                Logger.LogInformation($"searchvalue is {searchValue}");
                //Paging Size (10,20,50,100)  
                Int32 pageSize = length != null ? Convert.ToInt32(length) : 0;
                Int32 skip = start != null ? Convert.ToInt32(start) : 0;
                Int32 recordsTotal = 0;
                recordsTotal = queryData.Count();

                // Filtering
                if (whereClause != null)
                {
                    queryData = queryData.AsQueryable().Where(whereClause);
                }

                // Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    queryData = queryData.AsQueryable().OrderBy(sortColumn, sortColumnDirection);
                }

                //Paging   
                if (pageSize > 0)
                {
                    // paging is enabled
                    queryData = queryData.Skip(skip).Take(pageSize).ToList();
                }

                Logger.LogInformation($"querydata count is {queryData.Count()}");
                // Build the result 
                result = new DataTablesResult<T>
                {
                    Data = queryData,
                    Draw = int.Parse(draw),
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = queryData.Count()
                };
            }

            return result;
        }
    }
}
