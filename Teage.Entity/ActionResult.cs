using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    /// <summary>
    ///     操作动作 结果返回
    /// </summary>
    public class ActionResult
    {
        /// <summary>
        ///     ActionResult 构造器
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="message">操作的返回信息</param>
        public ActionResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        /// <summary>
        ///     ActionResult 构造器
        /// 默认IsSuccess为true
        /// </summary>
        public ActionResult()
        {
            IsSuccess = true;
        }

        /// <summary>
        ///     是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     反馈信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     错误编号
        /// </summary>
        public System.Enum ErrorCode
        {
            get { return _errorCode; }
            set
            {
                _errorCode = value;
                this.IsSuccess = false;
            }
        }

        private System.Enum _errorCode;
    }

    /// <summary>
    ///     Action 操作返回的反馈信息
    /// </summary>
    /// <typeparam name="TResult">ResultObject 的类型</typeparam>
    public class ActionResult<TResult> : ActionResult
    {
        /// <summary>
        ///     需要返回的对象
        /// </summary>
        public TResult ResultObject { get; set; }
    }
}
