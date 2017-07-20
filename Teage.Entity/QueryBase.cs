using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
     public abstract class QueryBase
    {
        public QueryBase()
        {
            OrderBy = "ID";
            OrderByType = "ASC";
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderByType { get; set; }
        /// <summary>
        /// 当前页数据排序字段
        /// </summary>
        public string SortName { get; set; }
        /// <summary>
        /// 当前页数据排序方式
        /// </summary>
        public string SortOrder { get; set; }
        /// <summary>
        /// 改变分页
        /// </summary>
        public string ChangePage { get; set; }

        /// <summary>
        /// 数据开始于
        /// </summary>
        private int _BeginNo = 0;
        public int BeginNo
        {
            get
            {
                _BeginNo = (Page - 1) * PageSize;
                return _BeginNo > 0 ? _BeginNo : 0;
            }
        }

        /// <summary>
        /// 数据结束于
        /// </summary>
        private int _EndNo = 1;
        public int EndNo
        {
            get
            {
                _EndNo = BeginNo + PageSize;
                return _EndNo > 0 ? _EndNo : int.MaxValue;
            }
        }
    }

    /// <summary>
    /// 没有查询条件时用这个查询
    /// </summary>
    public class QueryConcise : QueryBase
    {

    }
}

