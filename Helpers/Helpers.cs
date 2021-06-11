using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace BillTracker.Helpers
{
    public static class Helpers
    {
        public static IEnumerable<T2> ToViewModel<T1, T2>(this IEnumerable<T1> source) where T1 : class
        {
            return source.Select(c =>
            {
                return JsonConvert.DeserializeObject<T2>(JsonConvert.SerializeObject(c));
            });
        }

        public static T2 ToViewModelSingle<T1, T2>(this T1 source) where T1 : class
        {
            return JsonConvert.DeserializeObject<T2>(JsonConvert.SerializeObject(source));
        }

        //public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        //{
        //    DataTable dtReturn = new DataTable();

        //    // column names 
        //    PropertyInfo[] oProps = null;

        //    if (varlist == null) return dtReturn;

        //    foreach (T rec in varlist)
        //    {
        //        // Use reflection to get property names, to create table, Only first time, others will follow 
        //        if (oProps == null)
        //        {
        //            oProps = ((Type)rec.GetType()).GetProperties();
        //            foreach (PropertyInfo pi in oProps)
        //            {
        //                Type colType = pi.PropertyType;

        //                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
        //                {
        //                    colType = colType.GetGenericArguments()[0];
        //                }

        //                dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
        //            }
        //        }

        //        DataRow dr = dtReturn.NewRow();

        //        foreach (PropertyInfo pi in oProps)
        //        {
        //            dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
        //            (rec, null);
        //        }

        //        dtReturn.Rows.Add(dr);
        //    }
        //    return dtReturn;
        //}
    }
}