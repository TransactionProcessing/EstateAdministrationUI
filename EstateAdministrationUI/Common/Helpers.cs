namespace EstateAdministrationUI.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Areas.Estate.Models;
    using BusinessLogic.Common;
    using Microsoft.AspNetCore.Http;
    using Shared.Logger;

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
                Logger.LogDebug("got form");
                // Extract the data tables fields
                String draw = formData["draw"].FirstOrDefault();
                Logger.LogDebug($"draw is {draw}");
                // Skiping number of Rows count  
                String start = formData["start"].FirstOrDefault();
                Logger.LogDebug($"start is {start}");
                // Paging Length 10,20  
                String length = formData["length"].FirstOrDefault();
                Logger.LogDebug($"length is {length}");
                // Sort Column Name  
                String sortColumn = formData["columns[" + formData["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                Logger.LogDebug($"sortcol is {sortColumn}");
                // Sort Column Direction ( asc ,desc)  
                String sortColumnDirection = formData["order[0][dir]"].FirstOrDefault();
                Logger.LogDebug($"sortcoldirection is {sortColumnDirection}");
                // Search Value from (Search box)  
                String searchValue = formData["search[value]"].FirstOrDefault();
                Logger.LogDebug($"searchvalue is {searchValue}");
                
                Int32 pageSize = length != null ? Convert.ToInt32(length) : 0;
                Int32 skip = start != null ? Convert.ToInt32(start) : 0;
                
                Int32 recordsTotal = queryData.Count();
                Int32 filteredCount = recordsTotal;
                
                // Filtering
                if (whereClause != null)
                {
                    queryData = queryData.AsQueryable().Where(whereClause);
                    filteredCount = queryData.Count();
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

                Logger.LogDebug($"querydata count is {queryData.Count()}");

                // Build the result 
                result = new DataTablesResult<T>
                {
                    Data = queryData.ToList(),
                    Draw = int.Parse(draw),
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsTotal != filteredCount ? filteredCount : recordsTotal
                };
            }

            return result;
        }
    }
}
