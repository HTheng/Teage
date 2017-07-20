using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teage.Model;

namespace Teage.Entity
{
    public class Entity : TeageEntities
    {
        /// <summary>
        /// 通过sql语句查询数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="queryInfo"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public PageModel<T> QuerySql<T>(string sql, QueryBase queryInfo, params object[] parameters) where T : class,new()
        {
            var rtnModel = new PageModel<T>();
            rtnModel.Models = Database.SqlQuery<T>(RtnPageSql(sql, queryInfo), parameters).ToList();
            rtnModel.Total = Database.SqlQuery<int>(RtnPageSql(sql, parameters)).FirstOrDefault();

            return rtnModel;
        }

        #region 私有方法

        /// <summary>
        /// 获取分页sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        private string RtnPageSql(string sql, QueryBase queryInfo)
        {
            string sqlPage = "SELECT B.* FROM (SELECT ROW_NUMBER()OVER(ORDER BY " + queryInfo.OrderBy + " " + queryInfo.OrderByType + ")ROWNUMBER,A.* FROM (" + sql + @") A) B 
                            WHERE B.ROWNUMBER BETWEEN " + queryInfo.BeginNo + " AND " + queryInfo.EndNo;
            if (!string.IsNullOrEmpty(queryInfo.SortName) && !string.IsNullOrEmpty(queryInfo.SortOrder))
            {
                sqlPage = string.Format("{0} ORDER BY {1} {2}", sqlPage, queryInfo.SortName, queryInfo.SortOrder);
            }

            return sqlPage;
        }

        /// <summary>
        /// 获取数据总条数sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string RtnPageSql(string sql, dynamic parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    sql = sql.Replace(parameters[i].ParameterName, "'" + parameters[i].Value + "'");
                }
            }

            string sqlPage = "SELECT COUNT(*) FROM (" + sql + ") A";
            return sqlPage;
        }

        #endregion
    }
}
